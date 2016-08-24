using Ionic.Zip;
using ProcessDashboard.Accessors;
using ProcessDashboard.Accessors.FileAccess;
using ProcessDashboard.DataManagment;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.GenerationProcess {
    /// <summary>
    /// Class that Process all the Information from a given Student.
    /// </summary>
    public class StudentInformation {

        #region Fields        
        /// <summary>
        /// An Specific Student.
        /// </summary>
        public Student student { get; set; }
        /// <summary>
        /// The zip Content where Data is extracted from.
        /// </summary>
        public ZipFile zip { get; private set; }
        /// <summary>
        /// The Directory Where the Zip File from a Student is.
        /// </summary>
        public string directory { get; private set; }
        /// <summary>
        /// Contains all the Programs of a specific Student.
        /// </summary>
        public virtual List<ProgramInformation> programs { get; set; } = new List<ProgramInformation>();

        #endregion

        /// <summary>
        /// Prepares Data from the Zip File of a Student.
        /// </summary>
        /// <param name="directory">The directory where ZipFile of the Student is.</param>
        /// <param name="zipFile">The Zip Files where Data from the Student is.</param>
        public StudentInformation(string directory, string zipFile) {
            this.directory = directory;
            if (string.IsNullOrEmpty(zipFile) || !File.Exists(zipFile))
                throw new FileNotFoundException($"Cannot access the zip File {zipFile}.");
            this.zip = ZipFile.Read(zipFile);
        }

        #region Main Method
        /// <summary>
        /// Gets all the Information from the Student and adds it to the Data Manager.
        /// </summary>
        /// <param name="dataManager">Where all information is stored (Database or in a List).</param>
        public void generateInformation(IDataManager dataManager) {
            Phases previousPhases = null;
            Summary previousSummary = null;

            //Extracts the zip files, reads them and saves the information in the linker
            foreach (var project in Read<StateAccess, List<Data>>("state")) {
                var programInfo = new ProgramInformation(student, project["name"].getProgram());
                //Makes sure that the Program  is contained in the Options
                if (!programInfo.program.isValidProgram())
                    continue;
                programs.Add(programInfo);

                //Reads the data from .def and .dat
                var dataDef = Read<DefectAccess, List<Data>>(project["defectLog"]);
                var dataDat = Read<DatAccess, Data>(project["dataFile"]);

                //Gets all the information from the data read and saves it in the linker
                var defectLogs = programInfo.getDefectLogs(dataDef);
                dataManager.Add(defectLogs);
                var sets = programInfo.getSETS(dataDat);
                dataManager.Add(sets);
                var probe = programInfo.getPROBE(dataDat);
                dataManager.Add(probe);
                var phases = programInfo.getPhases(dataDat, sets, ref previousPhases, probe);
                dataManager.Add(phases);
                var summary = programInfo.getSummary(dataDat, sets, ref previousPhases, ref previousSummary);
                dataManager.Add(summary);
            }
            //Reads the data from Time Log and saves it in the linker
            var dataTimeLog1 = Read<TimeLogAccess, List<Data>>("timelog.xml");
            var dataTimeLog2 = Read<TimeLogAccess, List<Data>>("timelog2.xml");
            var timeLogs = getTimeLogs(dataTimeLog1).Concat(getTimeLogs(dataTimeLog2));
            dataManager.Add(timeLogs);
        }

        /// <summary>
        /// Gets the Time Logs Information from the timelog.xml File Data.
        /// </summary>
        /// <param name="dataTimeLog">Data from timelog.xml.</param>
        /// <returns>Returns the Time Logs in the Program.</returns>
        public IEnumerable<TimeLog> getTimeLogs(IEnumerable<Data> dataTimeLog) {
            //Takes information from the TimeLog Data and Returns it
            TimeLog previousTimeLog = null;
            foreach (Data data in dataTimeLog) {
                var timeLog = new TimeLog();
                timeLog.initialize(data, previousTimeLog);
                var program = programs.FirstOrDefault(x => x.program == timeLog.Program);
                if (program == null)
                    continue;
                program.initialize(timeLog);
                timeLog.populate();
                //If is a valid Program returns the new timeLog data
                if (timeLog.Program.isValidProgram()) {
                    previousTimeLog = timeLog;
                    yield return timeLog;
                }
            }
        }
        #endregion

        #region Read Data
        /// <summary>
        /// Gets the Name of the Student inside the Global File (global.dat).
        /// </summary>
        /// <param name="globalFile">Name of the Global File.</param>
        /// <returns>Returns the Name of the Student inside the File.</returns>
        public string getStudent(string globalFile) {
            string studentName = Read<DatAccess, Data>(globalFile)?["Owner"];
            if (string.IsNullOrEmpty(studentName))
                throw new KeyNotFoundException("Student Name could not be found");
            return studentName;
        }

        /// <summary>
        /// Gets Information from a File inside the Zip File.
        /// </summary>
        /// <typeparam name="TAccess">The Type of Access that will Read.</typeparam>
        /// <typeparam name="TResult">The Type of Result that will be Returned.</typeparam>
        /// <param name="fileName">The Name of the File that will Access in the Zip File.</param>
        /// <returns>The Information Read from the File.</returns>
        public TResult Read<TAccess, TResult>(string fileName) where TAccess : IReadableFileAccess<TResult> {
            var access = (TAccess)Activator.CreateInstance(typeof(TAccess), zip, directory, fileName);
            using (var reader = access) {
                return reader.Read();
            }
        }
        #endregion
    }
}
