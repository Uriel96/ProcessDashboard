using Microsoft.Data.Entity;
using ProcessDashboard.DataManagment;
using ProcessDashboard.GenerationProcess;
using ProcessDashboard.HelperClasses;
using ProcessDashboard.POCO;
using ProcessDashboard.POCO.POCO_SET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard {
    /// <summary>
    /// Form that Shows the Data stored in the Database.
    /// </summary>
    public partial class DataWindow : Form {
        #region Properties        
        /// <summary>
        /// The Last Program where Data will be shown.
        /// </summary>
        public Program ToProgram { get; set; }
        /// <summary>
        /// The Student Data that wants to be shown.
        /// </summary>
        public Student Student { get; set; }
        /// <summary>
        /// The Type of Table that will be shown.
        /// </summary>
        public Type tableType { get; set; }
        /// <summary>
        /// Whether it will show all the previous version or just the last one.
        /// </summary>
        public bool isPreviousVersion { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DataWindow"/> class.
        /// </summary>
        public DataWindow() {
            InitializeComponent();
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Shows the Specified Data from the Database in the visual table.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="studentName">The Name of the student that wants to be shown.</param>
        public void populate(DashboardContext db, string studentName) {
            DataTable dt = new DataTable();

            var properties = tableType.BaseType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            properties = properties.Concat(tableType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)).ToArray();

            var properties2 = new HashSet<PropertyInfo>(properties);
            foreach (PropertyInfo prop in properties2) {
                var getMethod = prop.GetGetMethod(false);
                if (getMethod.GetBaseDefinition() == getMethod) {
                    if (Nullable.GetUnderlyingType(prop.PropertyType) == null) {
                        dt.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
                    } else {
                        dt.Columns.Add(new DataColumn(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType)));
                    }
                }
            }
            var counter = 0;
            IEnumerable<DashboardTable> table = null;
            if (tableType == typeof(DefectLog)) {
                table = db.tableDL;
            } else if (tableType == typeof(TimeLog)) {
                table = db.tableTL;
            } else if (tableType == typeof(SET)) {
                table = db.tableSET;
            } else if (tableType == typeof(PROBE)) {
                table = db.tablePROBE;
            } else if (tableType == typeof(Summary)) {
                table = db.tableSummary;
            } else if (tableType == typeof(Phase)) {
                table = db.tablePhases;
            }
            if (table != null) {
                var query = table.Where((x => (x.Student.ToLower().StartsWith(studentName.ToLower()) && x.Program <= ToProgram && (!isPreviousVersion && x.Version == Student.LastVersion || isPreviousVersion))));
                foreach (var item in query) {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo prop in properties) {
                        var value = prop.GetValue(item);
                        if (value != null) {
                            row[prop.Name] = value;
                        }
                    }
                    dt.Rows.Add(row);
                    counter++;
                }
            }

            DataDB.DataSource = dt;
        }
        #endregion

        #region Controls        
        /// <summary>
        /// Sets the Initial Components States when the Window is shown.
        /// </summary>
        private void DataWindowLoad(object sender, EventArgs e) {
            using (DashboardContext db = new DashboardContext()) {
                foreach (Student student in db.tableStudents) {
                    CboStudent.Items.Add(student.Name);
                }
            }
            foreach (string program in ProcessDashboard.linkerTables.Keys) {
                CboTable.Items.Add(program);
            }
            foreach (Program program in Enum.GetValues(typeof(Program))) {
                CboToProgram.Items.Add(program.getProgram());
            }
        }

        /// <summary>
        /// Shows the Data in the visual table based on the query.
        /// </summary>
        private void BtnQueryClick(object sender, EventArgs e) {
            using (DashboardContext db = new DashboardContext()) {
                if (ProcessDashboard.linkerTables.ContainsKey(CboTable.SelectedItem as string)) {
                    Student = db.tableStudents.FirstOrDefault(x => x.Name == CboStudent.SelectedItem as string);
                    ToProgram = (CboToProgram.SelectedItem as string).getProgram();
                    isPreviousVersion = CbPreviousVersion.Checked;
                    tableType = ProcessDashboard.linkerTables[CboTable.SelectedItem as string];
                    if (Student != null) {
                        populate(db, Student.Name);
                    } else {
                        populate(db, CboStudent.SelectedItem as string);
                    }
                }
            }
        }
        #endregion
    }
}
