using Microsoft.Data.Entity;
using ProcessDashboard.POCO;
using ProcessDashboard.POCO.POCO_SET;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.DataManagment {
    /// <summary>
    /// Interface in Charge of Storing and Managing all the Information.
    /// </summary>
    public interface IDataManager {
        #region Properties        
        /// <summary>
        /// The Data of all the Students.
        /// </summary>
        IEnumerable<Student> students { get; }
        /// <summary>
        /// The Data of all the Defects Logged.
        /// </summary>
        IEnumerable<DefectLog> defectLogs { get; }
        /// <summary>
        /// The Data of all the Time Logs.
        /// </summary>
        IEnumerable<TimeLog> timeLogs { get; }
        /// <summary>
        /// The Data of all the Size Estimated Templates.
        /// </summary>
        IEnumerable<SET> sets { get; }
        /// <summary>
        /// The Data of all the PROBES.
        /// </summary>
        IEnumerable<PROBE> probes { get; }
        /// <summary>
        /// The Data of all the Phases.
        /// </summary>
        IEnumerable<Phase> phases { get; }
        /// <summary>
        /// The Data of all the Summaries.
        /// </summary>
        IEnumerable<Summary> summaries { get; }
        #endregion

        #region Setters
        /// <summary>
        /// Checks If Student exists, if exists Gets the Student and Creates a new Version, if not Adds a Student to the Data.
        /// </summary>
        /// <param name="studentName">The name of the Student that will be Added.</param>
        /// <returns>Returns the Added or Retrieved Student.</returns>
        Student AddOrGet(string studentName);
        /// <summary>
        /// Stores Time Logs from a Program in Data.
        /// </summary>
        /// <param name="dataTL">Time Logs from a Specific Program.</param>
        void Add(IEnumerable<TimeLog> dataTL);
        /// <summary>
        /// Stores Defect Logs from a Program in Data.
        /// </summary>
        /// <param name="dataDL">Time Logs from a Specific Program.</param>
        void Add(IEnumerable<DefectLog> dataDL);
        /// <summary>
        /// Stores SETs from a Program in Data.
        /// </summary>
        /// <param name="dataSET">SETs from a Specific Program.</param>
        void Add(SETS dataSET);
        /// <summary>
        /// Stores a PROBE from a Program in Data.
        /// </summary>
        /// <param name="probe">A PROBE from a Specific Program.</param>
        void Add(PROBE probe);
        /// <summary>
        /// Stores Phases from a Program in Data.
        /// </summary>
        /// <param name="dataPhases">Time Logs from a Specific Program.</param>
        void Add(IEnumerable<Phase> dataPhases);
        /// <summary>
        /// Stores a Summary from a Program in Data.
        /// </summary>
        /// <param name="summary">A Summary from a Specific Program.</param>
        void Add(Summary summary);
        #endregion
    }
}
