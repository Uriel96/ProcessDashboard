using ProcessDashboard.POCO;
using ProcessDashboard.Accessors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static ProcessDashboard.HelperClasses.DataUtil;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ProcessDashboard.Accessors.FileAccess;
using ProcessDashboard.POCO.POCO_SET;
using ProcessDashboard.DataManagment;
using ProcessDashboard.GenerationProcess;
using ProcessDashboard.HelperClasses;

namespace ProcessDashboard {

    /// <summary>
    /// Main Form where the Process is Done and Information is Generated
    /// </summary>
    public partial class ProcessDashboard : Form {

        /*TODO: Make Time Log work again.*/
        /*TODO: Use the Correct Exception Types of Create own.*/
        /*TODO: Implement all Exceptions needed.*/

        #region Properties
        /// <summary>
        /// The Directory File where Data is Located.
        /// </summary>
        public static string directory { get; private set; }
        /// <summary>
        /// The Excel Template File which serves as base for the Generated Excels.
        /// </summary>
        public static string excelFile { get; private set; }
        /// <summary>
        /// The Programs that the Process will ignore when Generating Data.
        /// </summary>
        public static HashSet<Program> ignoredPrograms { get; private set; } = new HashSet<Program>() { Program.StudentProfile, Program.Report };
        /// <summary>
        /// The Last Program that the Process will Generate.
        /// </summary>
        public static Program lastProgram { get; private set; }
        /// <summary>
        /// The Prefix used to indentify and find the Zip Files.
        /// </summary>
        public static string prefix { get; private set; }
        /// <summary>
        /// When true, Deletes all the Excel Files from the directories Zip Files.
        /// </summary>
        public static bool isDeleteExcels { get; set; }
        /// <summary>
        /// When true, Saves Data in the Database
        /// </summary>
        public static bool isSaveInDB { get; private set; }
        /// <summary>
        /// When true, Generates all the Excels from the Students
        /// </summary>
        public static bool isGenerateExcels { get; private set; }
        /// <summary>
        /// When true, Generates Information  from only Specific Student (in the directory)
        /// </summary>
        public static bool isGenerateOne { get; private set; }
        /// <summary>
        /// When true, Generates an Excel that Acumulates Information from all Students.
        /// </summary>
        public static bool isAcumulatedExcel { get; private set; }
        /// <summary>
        /// When true, Checks into every Student the number of Programs done.
        /// </summary>
        public static bool isAutomaticProgram { get; private set; } = true;
        /// <summary>
        /// Stablishes a link between the tables Name and the Type of Information it represents.
        /// </summary>
        public static Dictionary<string, Type> linkerTables { get; private set; } = new Dictionary<string, Type>() {
            { "BD", typeof(DefectLog) },
            { "BT", typeof(TimeLog) },
            { "SET", typeof(SET) },
            { "Fases", typeof(Phase) },
            { "PROBE", typeof(PROBE) },
            { "RES", typeof(Summary) },
        };

        #endregion

        #region Fields
        private const string configFileName = "Configuration.txt";
        private int numFiles;
        private Thread thread;
        #endregion

        /// <summary>
        /// Initializes Dashboard Visual Components.
        /// </summary>
        public ProcessDashboard() {
            InitializeComponent();
        }

        #region Main Methods
        /// <summary>
        /// Main Process that Sets Controls and Shows the Information when Finished or Canceled.
        /// </summary>
        public async void RunProcess1() {

            try {
                //Starts the Cronometer
                var cronometer = new Stopwatch();
                cronometer.Start();

                BtnExecute.Enabled = false;
                BtnCancel.Enabled = true;

                //Creates an Acumulative Excel depending on 'isAcumulatedExcel'
                if (isAcumulatedExcel) {
                    using (var acumulatedExcel = new DashboardExcel(excelFile, true)) {
                        acumulatedExcel.addStudentColumn();

                        await RunProcess2(acumulatedExcel);
                        //Saves the Acumulated Excel
                        acumulatedExcel.SaveAcumulated(directory);
                    }
                } else {
                    await RunProcess2(null);
                }
                //Shows a message telling it finished and the main Information about the Process
                cronometer.Stop();
                var realTime = cronometer.Elapsed;
                var message = new StringBuilder();
                message.AppendLine("El Proceso se llevó a cabo de manera existosa");
                message.AppendLine($"Tardó {realTime.Hours:00} : {realTime.Minutes:00} : {realTime.Seconds:00}");
                message.AppendLine();
                if (isGenerateExcels)
                    message.AppendLine($"Se generaron {numFiles} Exceles");
                if (isAcumulatedExcel)
                    message.AppendLine($"Se generaró un Excel Acumulativo");
                if (isSaveInDB)
                    message.AppendLine($"La información se guardó en la base de Datos");
                MessageBox.Show(message.ToString(), "El Proceso ha Terminado");
            } catch (IOException ex) {
                //Shows the problem and a suggested solution
                var message = new StringBuilder();
                message.AppendLine(ex.Message);
                message.AppendLine("Asegurate de que todos los Exceles necesarios en el proceso estén Cerrados.");
                MessageBox.Show(message.ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally {
                BtnExecute.Enabled = true;
                BtnCancel.Enabled = false;
            }
        }

        /// <summary>
        /// Process that Sets up the Database.
        /// </summary>
        /// <param name="acumulatedExcel">The Excel where all information from students is stored.</param>
        /// <returns>Returns the Task from the Process 3.</returns>
        public async Task RunProcess2(DashboardExcel acumulatedExcel) {
            //Initializes the Database
            if (isSaveInDB) {
                using (var db = new DashboardContext()) {
                    await RunProcess3(acumulatedExcel, db);
                }
            } else {
                await RunProcess3(acumulatedExcel, null);
            }
        }

        /// <summary>
        /// Process that generates Information from One or Multiple Students.
        /// </summary>
        /// <param name="acumulatedExcel">The Excel where all information from students is stored.</param>
        /// <param name="db">The Database used. If there is no database, dataList is used as default.</param>
        /// <returns>Returns the Task from all the generated students.</returns>
        public async Task RunProcess3(DashboardExcel acumulatedExcel, DashboardContext db) {
            //Initializes ProgressBar Values
            PbProcessGeneration.Minimum = 0;
            PbProcessGeneration.Maximum = numFiles;
            PbProcessGeneration.Value = 0;
            PbProcessGeneration.Step = 1;

            //Processes Information
            if (isGenerateOne) {
                string studentName = await Task.Factory.StartNew(() => {
                    return GenerateInformation(directory, acumulatedExcel, db);
                });
                LblInformation.Text = $"Información de {studentName} Terminado";
                PbProcessGeneration.PerformStep();
            } else {
                foreach (var subDirectory in Directory.GetDirectories(directory)) {
                    string studentName = await Task.Factory.StartNew(() => {
                        thread = Thread.CurrentThread;
                        return GenerateInformation(subDirectory, acumulatedExcel, db);
                    });
                    LblInformation.Text = $"Información de {studentName} Terminado";
                    PbProcessGeneration.PerformStep();
                }
            }
        }

        /// <summary>
        /// Generates all the Information from getting the Data to Saving it in Excel or/and the Database
        /// </summary>
        /// <param name="directory">The Path where the Data if the Student is.</param>
        /// <param name="acumulatedExcel">The Excel where all information from students is stored.</param>
        /// <param name="db">The Database used. If there is no database, dataList is used as default.</param>
        /// <returns>Returns the Name of the Student that its Information is beeing Generated.</returns>
        public string GenerateInformation(string directory, DashboardExcel acumulatedExcel, DashboardContext db) {
            //Gets the zip file
            string zipFile = Directory.GetFiles(directory).FirstOrDefault(file => file.startsWithPrefix(prefix));

            if (zipFile == null)
                return null;

            //Creates a linker to store and process data
            IDataManager link;
            if (db != null)
                link = db;
            else
                link = new DataList();

            //Gets the name of the student and adds it to the database
            var studentInfo = new StudentInformation(directory, zipFile);
            string studentName = studentInfo.getStudent("global.dat");
            studentInfo.student = link.AddOrGet(studentName);

            //Generates all the Information about the Student
            studentInfo.generateInformation(link);

            //Saves data to the database
            if (db != null) {
                ((DashboardContext)link).SaveChanges();
            }

            //Checks if Excels are going to be Generated
            if (isGenerateExcels || isAcumulatedExcel) {
                //Orders Data
                var allDefectLogs = link.defectLogs.order().ToList();
                var allTimeLogs = link.timeLogs.order().ToList();
                var allSETS = link.sets.order().ToList();
                var allPROBES = link.probes.order().ToList();
                var allPhases = link.phases.order().ToList();
                var allSummaries = link.summaries.order().ToList();

                if (isGenerateExcels) {
                    //Uses the Excel File to create a new Excel
                    using (DashboardExcel excel = new DashboardExcel(excelFile)) {
                        //Fills Data into the Excel
                        excel.fillAll(allDefectLogs, allTimeLogs, allSETS, allPROBES, allPhases, allSummaries);
                        //Saves As a New Excel
                        excel.Save(directory, studentInfo.programs.Count, studentInfo.student.Name);
                    }
                }

                if (isAcumulatedExcel) {
                    //Adds Data into the Acumulated Excel
                    acumulatedExcel?.fillAll(allDefectLogs, allTimeLogs, allSETS, allPROBES, allPhases, allSummaries);
                }
            }
            return studentInfo.student.Name;
        }
        #endregion

        #region Controls
        /// <summary>
        /// Sets the Initial Configuration when the Application Runs
        /// </summary>
        private void ProcessDashboardLoad(object sender, EventArgs e) {
            //Sets everything based on Configuration
            setPredifinedControls();
            getConfigurationData();
            setConfigurationControls();

            //Creates Database if it does not exist
            using (var db = new DashboardContext()) {
                db.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// Executes the Main Process; Reads Data, Generates Excels and Saves it in the Database
        /// </summary>
        private void BtnExecuteClick(object sender, EventArgs e) {

            setConfigurationProperties();

            //Estimate how much Time will the Process take
            numFiles = isGenerateOne ? 1 : Directory.GetDirectories(directory)
                .Count(subDirectory => Directory.GetFiles(subDirectory)
                .Any(file => file.startsWithPrefix(prefix)));

            /*TODO: Get Real Feedback for estimation*/
            if (!isAutomaticProgram) {
                float estimatedTime = calculateEstimatedTime();
                Console.WriteLine($"Tardará aproximadamente {estimatedTime}");
            } else {
                Console.WriteLine($"No se puede estimar el tiempo si se calcula automáticamente");
            }

            saveConfigurationData();

            RunProcess1();
        }

        /// <summary>
        /// Stops the Process, aborting the main process thread
        /// </summary>
        private void BtnCancelClick(object sender, EventArgs e) {
            var isCanceled = MessageBox.Show("¿Estás seguro de que querer terminar el proceso?", "Advertencia!", MessageBoxButtons.YesNo);
            if (isCanceled == DialogResult.Yes) {
                thread?.Abort();
            }
        }

        /// <summary>
        /// Opens a new Window that shows the information stored in the Database
        /// </summary>
        private void BtnDataDBClick(object sender, EventArgs e) {
            var dataWindow = new DataWindow();
            dataWindow.Show();
        }

        /// <summary>
        /// Sets the Directory where Data will be processed
        /// </summary>
        private void BtnDashboardDirectoryClick(object sender, EventArgs e) {
            var windowDirectory = new FolderBrowserDialog();
            windowDirectory.SelectedPath = directory;

            if (windowDirectory.ShowDialog() != DialogResult.OK)
                return;

            directory = TxtDashboardDirectory.Text = windowDirectory.SelectedPath;
        }

        /// <summary>
        /// Sets the Excel Template File
        /// </summary>
        private void BtnExcelFileClick(object sender, EventArgs e) {
            var windowFile = new OpenFileDialog();
            if (!string.IsNullOrEmpty(excelFile)) {
                windowFile.InitialDirectory = Path.GetDirectoryName(excelFile);
            }
            windowFile.Filter = "Excel files (*.xlsx, *.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
            windowFile.FilterIndex = 1;

            if (windowFile.ShowDialog() != DialogResult.OK)
                return;

            excelFile = TxtExcelFile.Text = windowFile.FileName;
        }

        private void RbToProgramCheckedChanged(object sender, EventArgs e) {
            PaToProgram.Enabled = isAutomaticProgram = RbToProgram.Checked;
        }

        private void CbGenerateOneCheckedChanged(object sender, EventArgs e) {
            if (CbGenerateOne.Checked) {
                CbAcumulatedExcel.Checked = false;
                CbAcumulatedExcel.Enabled = false;
            } else {
                CbAcumulatedExcel.Enabled = true;
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Gets the Information from the Configuration File into the Properites
        /// </summary>
        public void getConfigurationData() {
            //Reads Configuration.txt and Sets the Controls to the Predifined Values
            TxtAccessor configFile = null;
            try {
                configFile = new TxtAccessor(configFileName.getProjectFile());
                configFile.Read();
                if (configFile.data.ContainsKey("Directory")) {
                    directory = configFile.data["Directory"];
                }
                if (configFile.data.ContainsKey("ExcelFile")) {
                    excelFile = configFile.data["ExcelFile"];
                }
                if (configFile.data.ContainsKey("Prefix")) {
                    prefix = configFile.data["Prefix"];
                }
                if (configFile.data.ContainsKey("IgnoredPrograms")) {
                    ignoredPrograms.Clear();
                    foreach (var program in configFile.data["IgnoredPrograms"].Split('|').Select(x => x.Trim())) {
                        ignoredPrograms.Add(program.getProgram());
                    }
                }
                if (configFile.data.ContainsKey("ToProgram")) {
                    lastProgram = configFile.data["ToProgram"].getProgram();
                }
                if (configFile.data.ContainsKey("GenerateExcels")) {
                    isGenerateExcels = configFile.data["GenerateExcels"].parse<int>() != 0;
                }
                if (configFile.data.ContainsKey("SaveInBD")) {
                    isSaveInDB = configFile.data["SaveInBD"].parse<int>() != 0;
                }
                if (configFile.data.ContainsKey("DeleteExcels")) {
                    isDeleteExcels = configFile.data["DeleteExcels"].parse<int>() != 0;
                }
                if (configFile.data.ContainsKey("GenerateOne")) {
                    isGenerateOne = configFile.data["GenerateOne"].parse<int>() != 0;
                }
                if (configFile.data.ContainsKey("AcumulatedExcel")) {
                    isAcumulatedExcel = configFile.data["AcumulatedExcel"].parse<int>() != 0;
                }
                if (configFile.data.ContainsKey("AutomaticProgram")) {
                    isAutomaticProgram = configFile.data["AutomaticProgram"].parse<int>() != 0;
                }
            } catch {
                //Create a new Configuration File
                using (StreamWriter sw = File.CreateText(configFileName.getProjectFile())) {
                    //Do nothing
                }
            }
        }

        /// <summary>
        /// Sets the Properties to the Current State of the Controls
        /// </summary>
        public void setConfigurationProperties() {
            //Sets properties to the current configuration controls in the program
            directory = TxtDashboardDirectory.Text;
            excelFile = TxtExcelFile.Text;
            prefix = TxtPrefix.Text;
            lastProgram = CboToProgram.GetItemText(CboToProgram.SelectedItem).getProgram();
            isGenerateExcels = CbGenerateExcels.Checked;
            isSaveInDB = CbSaveInDB.Checked;
            isDeleteExcels = CbDeleteExcels.Checked;
            isGenerateOne = CbGenerateOne.Checked;
            isAcumulatedExcel = CbAcumulatedExcel.Checked;
            isAutomaticProgram = RbAutomaticProgram.Checked;
        }

        /// <summary>
        /// Sets the Controls to the Current State of the Properties
        /// </summary>
        public void setConfigurationControls() {
            //Sets controls to the current properties values in the program
            TxtDashboardDirectory.Text = directory;
            TxtExcelFile.Text = excelFile;
            TxtPrefix.Text = prefix;
            CboToProgram.SelectedIndex = CboToProgram.FindString(lastProgram.getProgram());
            CbGenerateExcels.Checked = isGenerateExcels;
            CbSaveInDB.Checked = isSaveInDB;
            CbDeleteExcels.Checked = isDeleteExcels;
            CbGenerateOne.Checked = isGenerateOne;
            CbAcumulatedExcel.Checked = isAcumulatedExcel;
            if (isAutomaticProgram)
                RbAutomaticProgram.Checked = true;
            else
                RbToProgram.Checked = true;
            PaToProgram.Enabled = RbToProgram.Checked;

            foreach (Program program in Enum.GetValues(typeof(Program))) {
                if (!ignoredPrograms.Contains(program))
                    CboToProgram.Items.Add(program.getProgram());
            }
        }

        /// <summary>
        /// Saves the Current State Properties in the Configuration File
        /// </summary>
        public void saveConfigurationData() {
            //Saves Properties States into the Configuration File
            TxtAccessor configFile = new TxtAccessor(configFileName.getProjectFile());
            configFile.data.Add("Directory", directory);
            configFile.data.Add("ExcelFile", excelFile);
            configFile.data.Add("Prefix", prefix);
            configFile.data.Add("IgnoredPrograms", "");
            var counter = 0;
            //Adds Ignored Program in this format: Student Profile | Report
            foreach (var program in ignoredPrograms) {
                configFile.data["IgnoredPrograms"] += program.getProgram();
                if (counter < ignoredPrograms.Count - 1) {
                    configFile.data["IgnoredPrograms"] += " | ";
                }
                counter++;
            }
            if ((int)lastProgram != -1)
                configFile.data.Add("ToProgram", lastProgram.getProgram());
            configFile.data.Add("GenerateExcels", isGenerateExcels ? "1" : "0");
            configFile.data.Add("SaveInBD", isSaveInDB ? "1" : "0");
            configFile.data.Add("DeleteExcels", isDeleteExcels ? "1" : "0");
            configFile.data.Add("GenerateOne", isGenerateOne ? "1" : "0");
            configFile.data.Add("AcumulatedExcel", isAcumulatedExcel ? "1" : "0");
            configFile.data.Add("AutomaticProgram", isAutomaticProgram ? "1" : "0");
            configFile.Save();
        }

        /// <summary>
        /// Sets the States of the Controls that will Appear when the Application is running
        /// </summary>
        public void setPredifinedControls() {
            //Sets Controls to Predifined States
            TxtDashboardDirectory.ReadOnly = true;
            TxtExcelFile.ReadOnly = true;
            BtnCancel.Enabled = false;
        }

        /// <summary>
        /// Operation that calculates the Etimated Time based on the number of files and programs, as well as generating or not Excel files. 
        /// </summary>
        /// <returns>Returns The Estimated Time the Process will Take</returns>
        public float calculateEstimatedTime() {
            int numPrograms = (int)lastProgram;
            return (isGenerateExcels ? numFiles * numPrograms * 2.14f : 0) + (isAcumulatedExcel ? numFiles * numPrograms * 2.14f : 0) + (isSaveInDB ? numFiles * numPrograms * 0.084f : 0);
        }
        #endregion
    }
}
