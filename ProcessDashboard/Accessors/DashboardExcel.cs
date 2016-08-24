using ProcessDashboard.POCO;
using ProcessDashboard.POCO.POCO_SET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.Accessors {
    /// <summary>
    /// Class that Access The Process Dashboard Excel Template.
    /// </summary>
    public class DashboardExcel : ExcelAccessor {

        #region Properties        
        /// <summary>
        /// Checks if the Excel to be generated is the Acumulated one.
        /// </summary>
        public bool isAcumulative { get; set; }
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardExcel"/> class.
        /// </summary>
        /// <param name="file">The Template Excel File that will Access.</param>
        /// <param name="acumulated">if set to <c>true</c> data will be Acumulated in one Excel File.</param>
        public DashboardExcel(string file, bool acumulated = false) : base(file) {
            this.isAcumulative = acumulated;
        }
        #endregion

        #region Fill        
        /// <summary>
        /// Fills all the Data for each Sheet.
        /// </summary>
        /// <param name="defectLogs">The defect logs data.</param>
        /// <param name="timeLogs">The time logs data.</param>
        /// <param name="sets">The sets data.</param>
        /// <param name="probes">The probes data.</param>
        /// <param name="phases">The phases data.</param>
        /// <param name="summaries">The summaries data.</param>
        public void fillAll(IEnumerable<DefectLog> defectLogs, IEnumerable<TimeLog> timeLogs, IEnumerable<SET> sets, IEnumerable<PROBE> probes, IEnumerable<Phase> phases, IEnumerable<Summary> summaries) {
            fillDL(defectLogs);
            fillTL(timeLogs);
            fillSET(sets);
            fillPROBE(probes);
            fillPhases(phases);
            fillSummary(summaries);
        }

        /// <summary>
        /// Fills Defect Logs data into its corresponding Sheet.
        /// </summary>
        /// <param name="defectLogs">The defect logs to be added.</param>
        public void fillDL(IEnumerable<DefectLog> defectLogs) {
            fill(defectLogs, "BD", new Mapper<DefectLog> {
                { "ID_Defecto", x => x.DefectID },
                { "Fecha", x => x.Date },
                { "Fase Inyectado", x => x.PhaseInjected },
                { "Fase Removido", x => x.PhaseRemoved },
                { "Tipo Defecto", x => x.DefectType },
                { "Fix Time", x => x.FixTime },
                { "Fix Count", x => x.FixCount },
                { "Fix Defect", x => x.FixDefect },
                { "Descripcion", x => x.Description }
            });
        }

        /// <summary>
        /// Fills Time Logs data into its corresponding Sheet.
        /// </summary>
        /// <param name="timeLogs">The time logs to be added.</param>
        public void fillTL(IEnumerable<TimeLog> timeLogs) {
            fill(timeLogs, "BT", new Mapper<TimeLog>()
            {
                { "Fase", x => x.Type.getPhase() },
                { "Inicio", x => x.Started },
                { "Fin", x => x.Ended },
                { "Espacio", x => x.Space },
                { "Interrupcion", x => x.Interrupt },
                { "Delta", x => x.Delta },
                { "Comentario", x => x.Comment }
            });
        }

        /// <summary>
        /// Fills sets data into its corresponding Sheet.
        /// </summary>
        /// <param name="sets">The sets to be added.</param>
        public void fillSET(IEnumerable<SET> sets) {
            fill(sets, "SET", new Mapper<SET>()
            {
                { "Nombre", x => x.Name },
                { "Tipo Parte", x => x.Type.getSET() },
                { "Est. Base", x => x.EstBase },
                { "Est. Deleted", x => x.EstDeleted },
                { "Est. Modified", x => x.EstModified },
                { "Est. Added", x => x.EstAdded },
                { "Est. Size", x => x.EstSize },
                { "Est.Tipo", x => x.EstType },
                { "Est. Items", x => x.EstItems },
                { "Est. Rel. Size", x => x.EstRelSize },
                { "Est. NR", x => x.EstNR },
                { "Act. Base", x => x.ActBase },
                { "Act. Deleted", x => x.ActDeleted },
                { "Act. Modified", x => x.ActModified },
                { "Act. Added", x => x.ActAdded },
                { "Act. Size", x => x.ActSize },
                { "Act. Items", x => x.ActItems },
                { "Act. NR", x => x.ActNR }
            });
        }

        /// <summary>
        /// Fills probes data into its corresponding Sheet.
        /// </summary>
        /// <param name="probes">The probes to be added.</param>
        public void fillPROBE(IEnumerable<PROBE> probes) {
            fill(probes, "PROBE", new Mapper<PROBE>()
            {
                { "Tam_Proxy", x => x.SizeProxy },
                { "Tam_Metodo", x => x.SizeMethod },
                { "Tam_Plan", x => x.SizePlan },
                { "Tam_r2", x => x.SizeR2 },
                { "Tam_b0", x => x.SizeB0 },
                { "Tam_b1", x => x.SizeB1 },
                { "Tam_Rango", x => x.SizeRange },
                { "Tam_UPI", x => x.SizeUPI },
                { "Tam_LPI", x => x.SizeLPI },
                { "Tie_Metodo", x => x.TimeMethod },
                { "Tie_Plan", x => x.TimePlan },
                { "Tie_r2", x => x.TimeR2 },
                { "Tie_b0", x => x.TimeB0 },
                { "Tie_b1", x => x.TimeB1 },
                { "Tie_Rango", x => x.TimeRange },
                { "Tie_UPI", x => x.TimeUPI },
                { "Tie_LPI", x => x.TimeLPI }
            });
        }

        /// <summary>
        /// Fills phases data into its corresponding Sheet.
        /// </summary>
        /// <param name="phases">The phases to be added.</param>
        public void fillPhases(IEnumerable<Phase> phases) {
            fill(phases, "Fases", new Mapper<Phase>()
            {
                { "Fase", x => x.Type.getPhase() },
                { "e_Min", x => x.EstMin },
                { "e_Def_Iny", x => x.EstDefInj },
                { "e_Def_Rem", x => x.EstDefRem },
                { "e_Yield", x => x.EstYield },
                { "r_Min", x => x.ActMin },
                { "r_Def_Iny", x => x.ActDefInj },
                { "r_Def_Rem", x => x.ActDefRem},
                { "r_Yield", x => x.ActYield }
            });
        }

        /// <summary>
        /// Fills summaries data into its corresponding Sheet.
        /// </summary>
        /// <param name="summaries">The summaries to be added.</param>
        public void fillSummary(IEnumerable<Summary> summaries) {
            fill(summaries, "RES", new Mapper<Summary>()
            {
                { "e_LOC", x => x.EstLOC },
                { "e_Min", x => x.EstMin },
                { "e_Def", x => x.EstDef },
                { "e_Prod", x => x.EstProd },
                { "e_Yield", x => x.EstYield},
                { "r_LOC", x => x.ActLOC },
                { "r_Min", x => x.ActMin },
                { "r_Def", x => x.ActDef},
                { "r_Prod", x => x.ActProd },
                { "r_Yield", x => x.ActYield },
                { "r_TamB", x => x.ActSizeB },
                { "r_TamD", x => x.ActSizeD },
                { "r_TamM", x => x.ActSizeM },
                { "r_TamA", x => x.ActSizeA },
                { "r_TamR", x => x.ActSizeR },
                { "r_TamT", x => x.ActSizeT },
                { "r_PQI", x => x.ActPQI },
                { "r_%DLDR", x => x.ActDLDR },
                { "r_%CR", x => x.ActCR},
                { "r_VelRev", x => x.ActRevVel},
                { "r_CPI", x => x.ActCPI},
                { "r_DensDef", x => x.ActDensDef},
                { "r_%Def_CyT", x => x.ActDefCT},
                { "r_AFR", x => x.ActAFR},
                { "r_DH_DLDR", x => x.ActDHDLDR},
                { "r_DH_CR", x => x.ActDHCR},
                { "r_DH_UT", x => x.ActDHUT},
                { "r_DRL_DLDR", x => x.ActDRLDLDR},
                { "r_DRL_CR", x => x.ActDRLCR}
            });
        }

        /// <summary>
        /// Fills data into its corresponding Sheet.
        /// </summary>
        /// <typeparam name="T">Type of Information to be Added in the Excel Sheet.</typeparam>
        /// <param name="list">The list of Data be Added.</param>
        /// <param name="sheet">The Excel Sheet where Data should be Added.</param>
        /// <param name="mapper">The Mapper that contains the functions to retrieve Data's Information.</param>
        public void fill<T>(IEnumerable<T> list, string sheet, Mapper<T> mapper) where T : DashboardTable {
            try {
                //Adds the Base Columns all Tables have
                mapper.Add("ID", data => getCurrentCounter() + 1);
                mapper.Add("Programa", data => data.Program.getProgram());
                if (isAcumulative && !mapper.ContainsKey("Alumno")) {
                    mapper.Add("Alumno", data => data.Student);
                }
                //Generates Data in the Excel Sheet
                SearchSheet(sheet);
                fill(list, mapper);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Save        
        /// <summary>
        /// Creates a New Excel with the Information of a Student.
        /// </summary>
        /// <param name="directory">The Directory where the Excel File will be Created.</param>
        /// <param name="maxPrograms">The number of Programs made by the Student.</param>
        /// <param name="studentName">The Name of the Student.</param>
        public void Save(string directory, int maxPrograms, string studentName) {
            int numExcels = 0;
            if (!isAcumulative) {
                Directory.GetFiles(directory).ToList().ForEach(file => {
                    if (file.isAnExcelFile() && file.startsWithPrefix($@"00 - P{maxPrograms} - {studentName}")) {
                        if (ProcessDashboard.isDeleteExcels) {
                            File.Delete(file);
                        } else {
                            modifyExcelName(file, ref numExcels);
                        }
                    }
                });
                if (numExcels > 0) {
                    base.Save($@"{directory}\00 - P{maxPrograms} - {studentName} ({numExcels + 1}).xlsx");
                } else {
                    base.Save($@"{directory}\00 - P{maxPrograms} - {studentName}.xlsx");
                }
            }
        }

        /// <summary>
        /// Creates a New Excel with the Information of every Student.
        /// </summary>
        /// <param name="directory">The Directory where the Excel File will be Created.</param>
        public void SaveAcumulated(string directory) {
            var fileName = "acumulado";
            if (isAcumulative) {
                int numExcelsAcumulated = 0;
                string rootDirectory = Directory.GetParent(directory).ToString();
                Directory.GetFiles(rootDirectory).ToList().ForEach(file => {
                    if (file.isAnExcelFile() && file.startsWithPrefix(fileName)) {
                        modifyExcelName(file, ref numExcelsAcumulated);
                    }
                });

                if (numExcelsAcumulated > 0) {
                    base.Save($@"{rootDirectory}\{fileName} ({numExcelsAcumulated + 1}).xlsx");
                } else {
                    base.Save($@"{rootDirectory}\{fileName}.xlsx");
                }
            }
        }
        #endregion

        #region Helper Methods                
        /// <summary>
        /// Adds a Student Column to every Sheet in the Acumulated Excel.
        /// </summary>
        public void addStudentColumn() {
            if (isAcumulative) {
                foreach (var table in ProcessDashboard.linkerTables.Keys) {
                    SearchSheet(table);
                    AddColumn("Alumno", 2);
                }
            }
        }

        /// <summary>
        /// Modifies the Name of an Excel based on the other Excels seting an specific number at the end of the Name File.
        /// </summary>
        /// <param name="file">The Excel File to Modify its Name.</param>
        /// <param name="numExcelFiles">The number of Excel Files found.</param>
        private void modifyExcelName(string file, ref int numExcelFiles) {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var regex = new Regex(@".*\((\d+)\)$");
            var stringValue = regex.Match(fileName).Groups[1].Value;

            if (!string.IsNullOrEmpty(stringValue)) {
                int num;
                int.TryParse(stringValue, out num);
                if (numExcelFiles < num) {
                    numExcelFiles = num;
                }
            } else {
                File.Move(file, $@"{ Path.GetDirectoryName(file)}\{ fileName } (1){ Path.GetExtension(file)}");
                if (numExcelFiles == 0) {
                    numExcelFiles++;
                }
            }
        }
        #endregion
    }
}
