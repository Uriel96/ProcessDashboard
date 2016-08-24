using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ProcessDashboard.POCO.POCO_SET;

namespace ProcessDashboard.DataManagment {
    /// <summary>
    /// Class in Charge of Storing and Managing Data in RAM via Lists.
    /// </summary>
    public class DataList : IDataManager {
        #region Variables
        private List<Student> tableStudents = new List<Student>();
        private List<DefectLog> tableDL = new List<DefectLog>();
        private List<TimeLog> tableTL = new List<TimeLog>();
        private List<SET> tableSET = new List<SET>();
        private List<PROBE> tablePROBE = new List<PROBE>();
        private List<Phase> tablePhases = new List<Phase>();
        private List<Summary> tableSummary = new List<Summary>();
        #endregion

        #region Properties        
        /// <summary>
        /// The Data of all the Students.
        /// </summary>
        public IEnumerable<Student> students {
            get { return tableStudents; }
        }
        /// <summary>
        /// The Data of all the Defects Logged.
        /// </summary>
        public IEnumerable<DefectLog> defectLogs {
            get { return tableDL; }
        }
        /// <summary>
        /// The Data of all the Time Logs.
        /// </summary>
        public IEnumerable<TimeLog> timeLogs {
            get { return tableTL; }
        }
        /// <summary>
        /// The Data of all the Size Estimated Templates.
        /// </summary>
        public IEnumerable<SET> sets {
            get { return tableSET; }
        }
        /// <summary>
        /// The Data of all the PROBES.
        /// </summary>
        public IEnumerable<PROBE> probes {
            get { return tablePROBE; }
        }
        /// <summary>
        /// The Data of all the Phases.
        /// </summary>
        public IEnumerable<Phase> phases {
            get { return tablePhases; }
        }
        /// <summary>
        /// The Data of all the Summaries.
        /// </summary>
        public IEnumerable<Summary> summaries {
            get { return tableSummary; }
        }
        #endregion

        #region Add        
        /// <summary>
        /// Stores Defect Logs from a Program in Data.
        /// </summary>
        /// <param name="dataDL">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<DefectLog> dataDL) => Add(tableDL, dataDL.ToList());
        /// <summary>
        /// Stores Time Logs from a Program in Data.
        /// </summary>
        /// <param name="dataTL">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<TimeLog> dataTL) => Add(tableTL, dataTL.ToList());
        /// <summary>
        /// Stores SETs from a Program in Data.
        /// </summary>
        /// <param name="dataSET">SETs from a Specific Program.</param>
        public void Add(SETS dataSET) => Add(tableSET, dataSET);
        /// <summary>
        /// Stores a PROBE from a Program in Data.
        /// </summary>
        /// <param name="probe">A PROBE from a Specific Program.</param>
        public void Add(PROBE probe) => Add(tablePROBE, probe);
        /// <summary>
        /// Stores Phases from a Program in Data.
        /// </summary>
        /// <param name="dataPhases">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<Phase> dataPhases) => Add(tablePhases, dataPhases.ToList());
        /// <summary>
        /// Stores a Summary from a Program in Data.
        /// </summary>
        /// <param name="summary">A Summary from a Specific Program.</param>
        public void Add(Summary summary) => Add(tableSummary, summary);
        /// <summary>
        /// Adds the Student to the Data.
        /// </summary>
        /// <param name="studentName">The name of the Student that will be Added.</param>
        /// <returns>
        /// Returns the Added Student.
        /// </returns>
        public Student AddOrGet(string studentName) {
            //Creates a new Student
            var currentStudent = new Student() { Name = studentName, LastVersion = 0 };
            tableStudents.Add(currentStudent);
            return currentStudent;
        }
        #endregion

        #region Helper Methods                
        /// <summary>
        /// Adds the specified data in a table.
        /// </summary>
        /// <typeparam name="T">The Type of Information to be Added.</typeparam>
        /// <param name="table">The Information Stored.</param>
        /// <param name="data">The Collection of Data to be Added.</param>
        public void Add<T>(List<T> table, List<T> data) where T : DashboardTable {
            if (data != null) {
                table.AddRange(data);
            }
        }
        /// <summary>
        /// Adds the specified data in a table.
        /// </summary>
        /// <typeparam name="T">The Type of Information to be Added.</typeparam>
        /// <param name="table">The Information Stored.</param>
        /// <param name="data">The Data to be Added.</param>
        public void Add<T>(List<T> table, T data) where T : DashboardTable {
            if (data != null) {
                table.Add(data);
            }
        }
        #endregion
    }
}
