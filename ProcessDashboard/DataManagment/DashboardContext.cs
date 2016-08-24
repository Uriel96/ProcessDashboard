using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using ProcessDashboard.POCO.POCO_SET;

namespace ProcessDashboard.DataManagment {

    /// <summary>
    /// Class in Charge of Storing and Managing Data in an SQLite Database.
    /// </summary>
    /// <seealso cref="DbContext" />
    /// <seealso cref="IDataManager" />
    public class DashboardContext : DbContext, IDataManager {
        #region Properties        
        /// <summary>
        /// The Current Student that its Added to the Data (before Saving it).
        /// </summary>
        /// <remarks>This will Probably be Removed.</remarks>
        public Student currentStudent { get; set; }

        /// <summary>
        /// All Stored Data in in Database from the table Students.
        /// </summary>
        public DbSet<Student> tableStudents { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Defect Logs.
        /// </summary>
        public DbSet<DefectLog> tableDL { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Time Logs.
        /// </summary>
        public DbSet<TimeLog> tableTL { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Base Size Estimated Templates.
        /// </summary>
        public DbSet<BaseSET> tableBaseSET { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Reused Size Estimated Templates.
        /// </summary>
        public DbSet<ReusedSET> tableReusedSET { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table New Size Estimated Templates.
        /// </summary>
        public DbSet<NewSET> tableNewSET { get; set; }
        /// <summary>
        /// All Stored Data in Database from All the Size Estimated Templates (Base, New and Reused).
        /// </summary>
        public IEnumerable<SET> tableSET {
            get {
                var baseSET = tableBaseSET.Cast<SET>().ToList();
                var newSET = tableNewSET.Cast<SET>().ToList();
                var reusedSET = tableReusedSET.Cast<SET>().ToList();
                return baseSET.Concat(newSET).Concat(reusedSET);
            }
        }
        /// <summary>
        /// All Stored Data in Database from the table PROBE.
        /// </summary>
        public DbSet<PROBE> tablePROBE { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Phases.
        /// </summary>
        public DbSet<Phase> tablePhases { get; set; }
        /// <summary>
        /// All Stored Data in Database from the table Summary.
        /// </summary>
        public DbSet<Summary> tableSummary { get; set; }

        /// <summary>
        /// Retrieves the Data from the table Students.
        /// </summary>
        public IEnumerable<Student> students {
            get { return tableStudents; }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table Defect Logs.
        /// </summary>
        public IEnumerable<DefectLog> defectLogs {
            get { return getData(tableDL); }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table Time Log.
        /// </summary>
        public IEnumerable<TimeLog> timeLogs {
            get { return getData(tableTL); }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table all the SETs (Base, New and Reused).
        /// </summary>
        public IEnumerable<SET> sets {
            get {
                var baseSET = getData(tableBaseSET).Cast<SET>().ToList();
                var newSET = getData(tableNewSET).Cast<SET>().ToList();
                var reusedSET = getData(tableReusedSET).Cast<SET>().ToList();
                return baseSET.Concat(newSET).Concat(reusedSET);
            }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table PROBE.
        /// </summary>
        public IEnumerable<PROBE> probes {
            get { return getData(tablePROBE); }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table Phases.
        /// </summary>
        public IEnumerable<Phase> phases {
            get { return getData(tablePhases); }
        }
        /// <summary>
        /// Retrieves the Data from the Current Student Information from the table Summary.
        /// </summary>
        public IEnumerable<Summary> summaries {
            get { return getData(tableSummary); }
        }
        #endregion

        #region Add        
        /// <summary>
        /// Stores Time Logs from a Program in Database.
        /// </summary>
        /// <param name="dataTL">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<TimeLog> dataTL) {
            if (dataTL != null)
                tableTL.AddRange(dataTL.OrderBy(item => item.Started));
        }
        /// <summary>
        /// Stores Defect Logs from a Program in Database.
        /// </summary>
        /// <param name="dataDL">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<DefectLog> dataDL) {
            if (dataDL == null)
                return;
            foreach (var item in dataDL) {
                if (item != null)
                    tableDL.Add(item);
            }
        }
        /// <summary>
        /// Stores SETs from a Program in Database.
        /// </summary>
        /// <param name="dataSET">SETs from a Specific Program.</param>
        public void Add(SETS dataSET) {
            if (dataSET == null)
                return;
            foreach (var item in dataSET) {
                if (item is BaseSET)
                    tableBaseSET.Add(item as BaseSET);
                else if (item is NewSET)
                    tableNewSET.Add(item as NewSET);
                else if (item is ReusedSET)
                    tableReusedSET.Add(item as ReusedSET);
                else
                    continue;
            }
        }
        /// <summary>
        /// Stores a PROBE from a Program in Database.
        /// </summary>
        /// <param name="probe">A PROBE from a Specific Program.</param>
        public void Add(PROBE probe) {
            if (probe != null)
                tablePROBE.Add(probe);
        }
        /// <summary>
        /// Stores Phases from a Program in Database.
        /// </summary>
        /// <param name="dataPhases">Time Logs from a Specific Program.</param>
        public void Add(IEnumerable<Phase> dataPhases) {
            if (dataPhases != null)
                tablePhases.AddRange(dataPhases);
        }
        /// <summary>
        /// Stores a Summary from a Program in Database.
        /// </summary>
        /// <param name="summary">A Summary from a Specific Program.</param>
        public void Add(Summary summary) {
            if (summary != null)
                tableSummary.AddRange(summary);
        }
        /// <summary>
        /// Checks If Student exists, if exists Gets the Student and Creates a new Version, if not Adds a Student to the Database.
        /// </summary>
        /// <param name="studentName">The name of the Student that will be Added.</param>
        /// <returns>
        /// Returns the Added or Retrieved Student.
        /// </returns>
        public Student AddOrGet(string studentName) {
            //Checks if Student is already Added
            currentStudent = tableStudents.FirstOrDefault(x => x.Name.Equals(studentName));
            if (currentStudent != null) {
                currentStudent.LastVersion++;
                //Cleans All the Previous Versions
                cleanPreviousVersions(tableDL);
                cleanPreviousVersions(tableTL);
                cleanPreviousVersions(tableBaseSET);
                cleanPreviousVersions(tableNewSET);
                cleanPreviousVersions(tableReusedSET);
                cleanPreviousVersions(tablePROBE);
            } else {
                //Creates a new Student in the Database
                currentStudent = new Student() { Name = studentName, LastVersion = 0 };
                tableStudents.Add(currentStudent);
            }
            return currentStudent;
        }
        #endregion

        #region Helper Methods        
        /// <summary>
        /// Gets the Data from the Current Student from the specified table.
        /// </summary>
        /// <typeparam name="T">The Type of Information to retrieve.</typeparam>
        /// <param name="table">The table Stored in Database.</param>
        /// <returns>Returns the Data from the table and the Current Student.</returns>
        public IEnumerable<T> getData<T>(DbSet<T> table) where T : DashboardTable {
            return table.Where((item => (item.Student.Equals(currentStudent.Name) && item.Version == currentStudent.LastVersion)));
        }

        /// <summary>
        /// Cleans the Previous Versions from the Current Student (keeps two versions only).
        /// </summary>
        /// <typeparam name="T">The Type of Information to clean up.</typeparam>
        /// <param name="table">The table Stored in Database.</param>
        public void cleanPreviousVersions<T>(DbSet<T> table) where T : DashboardTable {
            table.RemoveRange(table.Where((x => x.Student.Equals(currentStudent.Name) && x.Version < currentStudent.LastVersion - 1)));
        }
        #endregion

        /// <summary>
        /// Overrided method from DBContext. Connects to the SQLite Database, using dashboard.db as the database file.
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connection = new SqliteConnection(@"DataSource = dashboard.db");
            optionsBuilder.UseSqlite(connection);
        }
    }
}
