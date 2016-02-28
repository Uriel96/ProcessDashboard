using Ionic.Zip;
using ProcessDashboard.DatosTemporales;
using ProcessDashboard.Lectores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;

namespace ProcessDashboard
{

    public partial class ProcessDashboard : Form
    {
        /*
            Corregir Poder no Guardar en la Base de Datos y si Generar Excel

        */

        public static string directorio;
        public static string archivoExcel;
        public static HashSet<string> ignorarProgramas;
        public static string prefijo;
        public static bool borrarExceles;
        public static bool guardarBaseDatos;
        public static bool generarExceles;
        public static bool generarUno;

        public static HashSet<string> fases = new HashSet<string>() { "Planning", "Design", "Design Review", "Code", "Code Review", "Compile", "Test", "Postmortem" };

        public static DashboardContext db;
        private DateTime defaultDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public ProcessDashboard()
        {
            InitializeComponent();
        }

        private void ProcessDashboard_Load(object sender, EventArgs e)
        {
            TxtDirectorioDashboard.ReadOnly = true;
            TxtArchivoExcel.ReadOnly = true;
            BtnCancelar.Enabled = false;

            ReplicaLectorDat lector = new ReplicaLectorDat($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\Configuracion.txt");
            lector.Leer("=", "|");
            TxtDirectorioDashboard.Text = directorio = lector.datos["Directorio"];
            TxtArchivoExcel.Text = archivoExcel = lector.datos["ArchivoExcel"];
            TxtPrefijo.Text = prefijo = lector.datos["Prefijo"];
            ignorarProgramas = new HashSet<string>(lector.datos["Ignorar"].Split('|').Select(x => x.Trim()).ToArray());
            CbGenerarExcel.Checked = generarExceles = DataUtil.parse<int>(lector.datos["GenerarExcel"]) != 0;
            CbGuardarBaseDatos.Checked = guardarBaseDatos = DataUtil.parse<int>(lector.datos["GuardarBaseDatos"]) != 0;
            CbBorrarExcel.Checked = borrarExceles = DataUtil.parse<int>(lector.datos["BorrarExcel"]) != 0;
            CbGenerarUno.Checked = generarUno = DataUtil.parse<int>(lector.datos["GenerarUno"]) != 0;
        }

        #region Llenar Datos
        public void LlenarDatos(string directorio, int maxPrograma, Alumno alumno)
        {
            LectorExcel lectorExcel = null;
            try
            {
                lectorExcel = new LectorExcel(archivoExcel);

                llenarBD(lectorExcel, db.tablaBD.Where(x => x.Alumno.Equals(alumno.Nombre) && x.Version == alumno.UltimaVersion).OrderBy(x => x.Programa).ThenBy(x => x.Fecha).ToList());
                llenarBT(lectorExcel, db.tablaBT.Where(x => x.Alumno.Equals(alumno.Nombre) && x.Version == alumno.UltimaVersion).OrderBy(x => x.Programa).ThenBy(x => x.Inicio).ToList());
                llenarSET(lectorExcel, db.tablaSET.Where(x => x.Alumno.Equals(alumno.Nombre) && x.Version == alumno.UltimaVersion).OrderBy(x => x.Programa).ThenBy(x => x.TipoParte).ToList());
                llenarPROBE(lectorExcel, db.tablaPROBE.Where(x => x.Alumno.Equals(alumno.Nombre) && x.Version == alumno.UltimaVersion).OrderBy(x => x.Programa).ToList());

                lectorExcel.Guardar($@"{directorio}\00 - P{maxPrograma} - {alumno.Nombre}.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                lectorExcel?.Cerrar();
            }
        }

        public void llenarBD(LectorExcel lectorExcel, List<BitacoraDefectos> lista)
        {
            try
            {
                int contador = 1;
                lectorExcel.BuscarHoja("BD");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<BitacoraDefectos, dynamic>>()
                {
                    { "ID_BD", x => contador++ },
                    { "Programa", x => x.Programa },
                    { "ID_Defecto", x => x.IDDefecto },
                    { "Fecha", x => x.Fecha },
                    { "Fase Inyectado", x => x.FaseInyectado },
                    { "Fase Removido", x => x.FaseRemovido },
                    { "Tipo Defecto", x => x.TipoDefecto },
                    { "Fix Time", x => x.FixTime },
                    { "Fix Count", x => x.FixCount },
                    { "Fix Defect", x => x.FixDefect },
                    { "Descripcion", x => x.Descripcion }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void llenarBT(LectorExcel lectorExcel, List<BitacoraTiempo> lista)
        {
            try
            {
                int contador = 1;
                DateTime? fin = null;
                string anteriorPrograma = "";
                lectorExcel.BuscarHoja("BT");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<BitacoraTiempo, dynamic>>()
                {
                    { "ID_BT", x => contador++ },
                    { "Programa", x => {  return x.Programa; } },
                    { "Fase", x => x.Fase },
                    { "Inicio", x => x.Inicio },
                    { "Fin", x => x.Fin },
                    { "Espacio",
                        x => {
                            float? espacio = anteriorPrograma.Equals(x.Programa) ? (float?)(x.Inicio - fin)?.TotalMinutes : null;
                            fin = x.Fin;
                            anteriorPrograma = x.Programa;
                            return espacio;
                        }
                    },
                    { "Interrupcion", x => x.Interrupcion },
                    { "Delta", x => x.Delta },
                    { "Comentario", x => x.Comentario }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void llenarSET(LectorExcel lectorExcel, List<SET> lista)
        {
            try
            {
                int contador = 1;
                lectorExcel.BuscarHoja("SET");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<SET, dynamic>>()
                {
                    { "ID_SET", x => contador++ },
                    { "Programa", x => x.Programa },
                    { "Nombre", x => x.Nombre },
                    { "Tipo Parte", x => x.TipoParte },
                    { "Est. Base", x => x.EstBase },
                    { "Est. Deleted", x => x.EstDeleted },
                    { "Est. Modified", x => x.EstModified },
                    { "Est. Added", x => x.EstAdded },
                    { "Est. Size", x => x.EstSize },
                    { "Est.Tipo", x => x.EstTipo },
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void llenarPROBE(LectorExcel lectorExcel, List<PROBE> lista)
        {
            try
            {
                lectorExcel.BuscarHoja("PROBE");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<PROBE, dynamic>>()
                {
                    { "Programa", x => x.Programa },
                    { "Tam_Proxy", x => x.TamProxy },
                    { "Tam_Metodo", x => x.TamMetodo },
                    { "Tam_Plan", x => x.TamPlan },
                    { "Tam_r2", x => x.TamR2 },
                    { "Tam_b0", x => x.TamB0 },
                    { "Tam_b1", x => x.TamB1 },
                    { "Tam_Rango", x => x.TamRango },
                    { "Tam_UPI", x => x.TamUPI },
                    { "Tam_LPI", x => x.TamLPI },
                    { "Tie_Metodo", x => x.TieMetodo },
                    { "Tie_Plan", x => x.TiePlan },
                    { "Tie_r2", x => x.TieR2 },
                    { "Tie_b0", x => x.TieB0 },
                    { "Tie_b1", x => x.TieB1 },
                    { "Tie_Rango", x => x.TieRango },
                    { "Tie_UPI", x => x.TieUPI },
                    { "Tie_LPI", x => x.TieLPI }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void llenarFases(LectorExcel lectorExcel, List<Fases> lista)
        {
            try
            {
                int contador = 1;
                lectorExcel.BuscarHoja("Fases");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<Fases, dynamic>>()
                {
                    { "ID", x => contador++ },
                    { "Programa", x => x.Programa },
                    { "Fase", x => x.Fase },
                    { "e_Min", x => x.EstMin },
                    { "e_Def_Iny", x => x.EstDefIny },
                    { "e_Def_Rem", x => x.EstDefRem },
                    { "e_Yield", x => x.EstYield },
                    { "r_Min", x => x.ResMin },
                    { "r_Def_Iny", x => x.ResDefIny },
                    { "r_Def_Rem", x => x.ResDefRem},
                    { "r_Yield", x => x.ResYield }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void llenarResumen(LectorExcel lectorExcel, List<Resumen> lista)
        {
            try
            {
                lectorExcel.BuscarHoja("RES");
                lectorExcel.LlenarDatos(lista, new Dictionary<string, Func<Resumen, dynamic>>()
                {
                    { "Programa", x => x.Programa },
                    { "e_LOC", x => x.EstLOC },
                    { "e_Min", x => x.EstMin },
                    { "e_Def", x => x.EstDef },
                    { "e_Prod", x => x.EstProd },
                    { "e_Yield", x => x.EstYield},
                    { "r_LOC", x => x.ResLOC },
                    { "r_Min", x => x.ResMin },
                    { "r_Def", x => x.ResDef},
                    { "r_Prod", x => x.ResYield },
                    { "r_Yield", x => x.ResYield },
                    { "r_PQI", x => x.ResPQI },
                    { "r_%DLDR", x => x.ResDLDR },
                    { "r_%CR", x => x.ResCR},
                    { "r_VelRev", x => x.ResVelRev},
                    { "r_CPI", x => x.ResCPI},
                    { "r_DensDef", x => x.ResDensDef},
                    { "r_%Def_CyT", x => x.ResDefCyT},
                    { "r_AFR", x => x.ResAFR},
                    { "r_DH_DLDR", x => x.ResDHDLDR},
                    { "r_DH_CR", x => x.ResDHCR},
                    { "r_DH_UT", x => x.ResDHUT},
                    { "r_DRL_DLDR", x => x.ResDRLDLDR},
                    { "r_DRL_CR", x => x.ResDRLCR}
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        private void BtnDirectorioDashboard_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog ventanaDirectorio = new FolderBrowserDialog();
            ventanaDirectorio.SelectedPath = directorio;

            if (ventanaDirectorio.ShowDialog() != DialogResult.OK) return;

            TxtDirectorioDashboard.Text = ventanaDirectorio.SelectedPath;
            directorio = ventanaDirectorio.SelectedPath;
        }

        private void BtnArchivoExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ventanaArchivo = new OpenFileDialog();
            ventanaArchivo.InitialDirectory = Path.GetDirectoryName(archivoExcel);
            ventanaArchivo.Filter = "Excel files (*.xlsx, *.xls)|*.xlsx;*.xls|All files (*.*)|*.*";
            ventanaArchivo.FilterIndex = 1;

            if (ventanaArchivo.ShowDialog() != DialogResult.OK) return;

            TxtArchivoExcel.Text = ventanaArchivo.FileName;
            archivoExcel = ventanaArchivo.FileName;
        }

        private void BtnEjecutar_Click(object sender, EventArgs e)
        {
            prefijo = TxtPrefijo.Text;
            generarExceles = CbGenerarExcel.Checked;
            guardarBaseDatos = CbGuardarBaseDatos.Checked;
            borrarExceles = CbBorrarExcel.Checked;
            generarUno = CbGenerarUno.Checked;

            int archivos = Directory.GetDirectories(directorio).Count(temp => Directory.GetFiles(temp).Any(x => Path.GetFileName(x).ToLower().StartsWith(prefijo)));
            int tiempo = archivos * 15;
            Console.WriteLine($"Tardará aproximadamente {tiempo}");
            ReplicaLectorDat lector = new ReplicaLectorDat($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\Configuracion.txt");
            lector.datos.Add("Directorio", directorio);
            lector.datos.Add("ArchivoExcel", archivoExcel);
            lector.datos.Add("Prefijo", prefijo);
            lector.datos.Add("GenerarExcel", generarExceles ? "1" : "0");
            lector.datos.Add("GuardarBaseDatos", guardarBaseDatos ? "1" : "0");
            lector.datos.Add("BorrarExcel", borrarExceles ? "1" : "0");
            lector.datos.Add("GenerarUno", generarUno ? "1" : "0");
            lector.Guardar("=", "|");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            using (db = new DashboardContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                if (generarUno)
                {
                    GenerarInformacion(directorio);
                }
                else
                {
                    foreach (var subDirectorio in Directory.GetDirectories(directorio))
                    {
                        GenerarInformacion(subDirectorio);
                    }
                }
            }

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;

            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{(ts.Milliseconds / 10):00}";
            Console.WriteLine(elapsedTime);
        }

        public void GenerarInformacion(string directorio)
        {
            string archivo = Directory.GetFiles(directorio).buscarModificar(x => Path.GetFileName(x).ToLower().StartsWith(prefijo),
            (x) =>
            {
                if (borrarExceles && Path.GetExtension(x).ToLower().Equals(".xlsx"))
                    File.Delete(x);
            });
            Linked link;
            if (guardarBaseDatos)
                link = new DashboardContext();
            else
                link = new ListedData();
            link.da = new DatosAlumno(directorio, archivo);

            string nombreAlumno = link.da.buscarAlumno("global.dat");

            LblInformacion.Text = $"Generando Información de {nombreAlumno}";
            //Checar si el alumno existe o no en la base de datos
            link.AgregarAlumno(nombreAlumno);

            var fasesTiempoAFecha = fases.ToDictionary(x => x, x => 0.0f);
            var fasesInyectadasAFecha = fases.ToDictionary(x => x, x => 0.0f);

            //Leer la información de cada proyecto y guardarla en la base de datos
            link.da.obtenerProyectos(link.da.leerState(archivo), programa => ignorarProgramas.Contains(programa), (proyecto) =>
            {
                var datosDef = link.da.leerDef(proyecto["defectLog"]);
                var datosDat = link.da.leerDat(proyecto["dataFile"]);

                var datosBD = link.da.obtenerBD(datosDef);
                var datosSET = link.da.obtenerSET(datosDat);
                var datosPROBE = link.da.obtenerPROBE(datosDat);
                var datosFases = link.da.obtenerFases(datosDat, fasesTiempoAFecha, fasesInyectadasAFecha, datosPROBE);

                link.AgregarDatos(null, datosBD, datosSET, datosPROBE, datosFases);
            });

            //Leer y guardar en la base de datos los datos de la Bitacora de Tiempo
            var datosLectorBT1 = link.da.leerTimeLog("timelog.xml");
            var datosLectorBT2 = link.da.leerTimeLog("timelog2.xml");

            var datosBT = link.da.obtenerBT(datosLectorBT1, bt => !ignorarProgramas.Contains(bt.Programa));
            datosBT.Concat(link.da.obtenerBT(datosLectorBT2, bt => !ignorarProgramas.Contains(bt.Programa)));

            link.AgregarDatos(datosBT);

            if (guardarBaseDatos) ((DashboardContext)link).SaveChanges();

            if (!generarExceles) return;

            LectorExcel lectorExcel = null;
            try
            {
                lectorExcel = new LectorExcel(archivoExcel);

                llenarBD(lectorExcel, link.obtenerBD().ToList());
                llenarBT(lectorExcel, link.obtenerBT().ToList());
                llenarSET(lectorExcel, link.obtenerSET().ToList());
                llenarPROBE(lectorExcel, link.obtenerPROBE().ToList());
                llenarFases(lectorExcel, link.obtenerFases().ToList());

                lectorExcel.Guardar($@"{directorio}\00 - P{link.da.maxProgramas} - {link.da.Alumno.Nombre}.xlsx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                lectorExcel?.Cerrar();
            }
        }

    }
}
