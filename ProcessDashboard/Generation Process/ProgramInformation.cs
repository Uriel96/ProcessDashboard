using Ionic.Zip;
using ProcessDashboard.Accessors;
using ProcessDashboard.Accessors.FileAccess;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;
using ProcessDashboard.POCO.POCO_SET;
using ProcessDashboard.HelperClasses;

namespace ProcessDashboard.GenerationProcess {
    /// <summary>
    /// The Different Number of Programs.
    /// </summary>
    public enum Program {
        /// <summary>
        /// The Student Main Information
        /// </summary>
        StudentProfile,
        /// <summary>
        /// The First Program.
        /// </summary>
        Program1,
        /// <summary>
        /// The Second Program.
        /// </summary>
        Program2,
        /// <summary>
        /// The Third Program.
        /// </summary>
        Program3,
        /// <summary>
        /// The Fourth Program.
        /// </summary>
        Program4,
        /// <summary>
        /// The Fifth Program.
        /// </summary>
        Program5,
        /// <summary>
        /// The Sixth Program.
        /// </summary>
        Program6,
        /// <summary>
        /// The Seventh Program.
        /// </summary>
        Program7,
        /// <summary>
        /// The Report of all the Programs.
        /// </summary>
        Report
    };

    /// <summary>
    /// Class in Charge of Processing the Information from a Specific Program.
    /// </summary>
    public class ProgramInformation {
        #region Properties        
        /// <summary>
        /// A reference of the Student from which the program is.
        /// </summary>
        public virtual Student student { get; set; }
        /// <summary>
        /// An Specific Program which Information is needed.
        /// </summary>
        public Program program { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes properties from a given Program.
        /// </summary>
        /// <param name="student">The Student from which Information is read.</param>
        /// <param name="program">An specific Program needed to generate Information.</param>
        public ProgramInformation(Student student, Program program) {
            this.student = student;
            this.program = program;
        }
        #endregion

        #region Get Data
        /// <summary>
        /// Gets the SETs Information from the .dat File Data.
        /// </summary>
        /// <param name="dataDat">Data (.dat) from the program.</param>
        /// <returns>Returns the Collection of SETs (Base, New and Reused)</returns>
        public SETS getSETS(Data dataDat) {
            var sets = new SETS(dataDat);
            sets.populate(set => {
                initialize(set);
            });
            return sets;
        }

        /// <summary>
        /// Gets the PROBE Information from the .dat File Data.
        /// </summary>
        /// <param name="dataDat">Data (.dat) from the program.</param>
        /// <returns>Returns a PROBE with the Information from Data.</returns>
        public PROBE getPROBE(Data dataDat) {
            //Creates PROBE and Populates it with the Data
            var probe = new PROBE();
            probe.initialize(dataDat);
            initialize(probe);
            probe.populate();
            return probe;
        }

        /// <summary>
        /// Gets the Defect Logs Information from the .def File Data.
        /// </summary>
        /// <param name="dataDef">Data (.def) from the program.</param>
        /// <returns>Returns the Defect Logs in the program.</returns>
        public IEnumerable<DefectLog> getDefectLogs(IEnumerable<Data> dataDef) {
            if (dataDef == null)
                yield break;

            //Takes information from the Defects Data and Returns it
            foreach (Data data in dataDef) {
                var defectLog = new DefectLog();
                defectLog.initialize(data);
                initialize(defectLog);
                defectLog.populate();
                yield return defectLog;
            }
        }

        /// <summary>
        /// Gets the Time Logs Information from the timelog.xml File Data.
        /// </summary>
        /// <param name="dataTimeLog">Data from timelog.xml.</param>
        /// <param name="condition">Condition in which Data will be processeed or not.</param>
        /// <returns>Returns the Time Logs in the Program.</returns>
        public IEnumerable<TimeLog> getTimeLogs(IEnumerable<Data> dataTimeLog, Predicate<TimeLog> condition) {
            //Takes information from the TimeLog Data and Returns it
            TimeLog previousTimeLog = null;
            foreach (Data data in dataTimeLog) {
                var timeLog = new TimeLog();
                timeLog.initialize(data, previousTimeLog);
                initialize(timeLog);
                timeLog.populate();
                //If condition is true it returns the new timeLog data
                if (condition(timeLog)) {
                    previousTimeLog = timeLog;
                    yield return timeLog;
                }
            }
        }

        /// <summary>
        /// Gets the Phases Information from the .dat File Data.
        /// </summary>
        /// <param name="dataDat">Data (.dat) from the program.</param>
        /// <param name="sets">SETs Information from the same program.</param>
        /// <param name="previousPhases">Phases Information from previous program.</param>
        /// <param name="probe">PROBE Information from the same program.</param>
        /// <returns>Returns the Phases in the Program.</returns>
        public IEnumerable<Phase> getPhases(Data dataDat, SETS sets, ref Phases previousPhases, PROBE probe) {
            //Creates new Phases
            var phases = new Phases(dataDat, sets, probe, previousPhases);
            //Populates the Data from SETS, Probe, and the previous Phases
            phases.populate(phase => initialize(phase));
            //Sets phases as the previous for the next time method is invoked
            previousPhases = phases;
            return phases;
        }

        /// <summary>
        /// Gets the Summary Information from Several Sources of Information.
        /// </summary>
        /// <param name="dataDat">Data (.dat) from the program.</param>
        /// <param name="sets">SETs Information from the same program.</param>
        /// <param name="previousPhases">Phases Information from previous program.</param>
        /// <param name="previousSummary">Summary Information from previous program.</param>
        /// <returns>Returns the Summary in the Program.</returns>
        public Summary getSummary(Data dataDat, SETS sets, ref Phases previousPhases, ref Summary previousSummary) {
            //Creates a new Summary
            Summary summary = new Summary();
            summary.initialize(dataDat, previousPhases, sets, previousSummary);
            initialize(summary);
            //Populates Data from SET, previous Phases and the previous Summary
            summary.populate();
            //Sets summary as the previous for the next time method is invoked
            previousSummary = summary;
            return summary;
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Initializes the Base Properties of the Information (Student, Version and Program).
        /// </summary>
        /// <typeparam name="T">The Type of Information to be Initialized.</typeparam>
        /// <param name="item">The Information that will Add the Base information.</param>
        public void initialize<T>(T item) where T : DashboardTable {
            item.Student = student.Name;
            item.Version = student.LastVersion;
            item.Program = program;
        }
        #endregion
    }
}
