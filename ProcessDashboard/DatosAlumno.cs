using Ionic.Zip;
using ProcessDashboard.DatosTemporales;
using ProcessDashboard.Lectores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public class DatosAlumno
    {
        public Alumno Alumno { get; set; }
        public string directorio { get; private set; }
        private DateTime defaultDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private ZipFile zip;
        public int maxProgramas { get; private set; } = 0;
        public string programaActual { get; set; }

        public DatosAlumno(string directorio, string archivoZip)
        {
            this.directorio = directorio;
            this.zip = ZipFile.Read(archivoZip);
        }

        #region Leer Datos

        public string buscarAlumno(string archivo)
        {
            string nombre = leerArchivo<ReplicaLectorDat, string>(archivo, (lectorGlobal) =>
            {
                //Buscar el nombre del alumno en el archivo
                return lectorGlobal.buscarDato("==#|\\?=|=@|=\"|==|=", "Owner");
            });

            if (nombre == null) throw new KeyNotFoundException("No se pudo encontrar el nombre del alumno");
            return nombre;
        }

        public IEnumerable<LectorXML.Atributos> leerState(string archivoZip)
        {
            try
            {
                return leerArchivo2<LectorXML, LectorXML.Atributos>("state", (lectorState) =>
                {
                    return lectorState.LeerDesde("node", "templateID", "pspCourseRoot", "name", "dataFile", "defectLog").ToList();
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public void obtenerProyectos(IEnumerable<LectorXML.Atributos> datosState, Predicate<string> condicion, Action<LectorXML.Atributos> accion)
        {
            foreach (var proyectos in datosState)
            {
                programaActual = proyectos["name"];
                //Checar que el programa esté contenido en las opciones
                if (programaActual == null || condicion(programaActual)) continue;
                maxProgramas++;
                accion(proyectos);
            }
        }

        public IEnumerable<LectorXML.Atributos> leerTimeLog(string archivo)
        {
            try
            {
                return leerArchivo2<LectorXML, LectorXML.Atributos>(archivo, (lectorBT) =>
                {
                    return lectorBT.Leer("time", "id", "path", "start", "delta", "interrupt", "comment");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return Enumerable.Empty<LectorXML.Atributos>();
        }

        public IEnumerable<LectorXML.Atributos> leerDef(string archivo)
        {
            return leerArchivo2<LectorXML, LectorXML.Atributos>(archivo, (lectorBD) =>
            {
                try
                {
                    return lectorBD.Leer("defect", "num", "date", "inj", "rem", "type", "ft", "count", "fd", "desc");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return Enumerable.Empty<LectorXML.Atributos>();
            });
        }

        public ReplicaLectorDat.Datos leerDat(string archivo)
        {
            try
            {
                return leerArchivo<ReplicaLectorDat, ReplicaLectorDat.Datos>(archivo, (lectorDat) =>
                {
                    lectorDat.Leer("==#|\\?=|=@|=\"|==|=");
                    return lectorDat.datos;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        #endregion

        #region Utilidades

        public M leerArchivo<T, M>(string archivo, Func<T, M> func) where T : Lector
        {
            //Checa si existe el archivo
            if (!zip.obtenerArchivo(directorio, archivo)) throw new FileNotFoundException($"No se pudo encontrar {archivo} en la dirección {directorio}");
            T temp = null;
            M resultado = default(M);
            try
            {
                temp = (T)Activator.CreateInstance(typeof(T), new object[] { $@"{directorio}\{archivo}", false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (temp != null) resultado = func(temp);

            //Borrar archivo
            File.Delete($@"{ directorio }\{ archivo }");
            return resultado;
        }

        public IEnumerable<M> leerArchivo2<T, M>(string archivo, Func<T, IEnumerable<M>> func) where T : Lector
        {
            //Checa si existe el archivo
            if (!zip.obtenerArchivo(directorio, archivo)) throw new FileNotFoundException($"No se pudo encontrar {archivo} en la dirección {directorio}");
            T temp = null;
            try
            {
                temp = (T)Activator.CreateInstance(typeof(T), new object[] { $@"{directorio}\{archivo}", false });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (temp != null)
            {
                foreach (var la in func(temp))
                {
                    yield return la;
                }
            }

            //Borrar archivo
            File.Delete($@"{ directorio }\{ archivo }");
        }

        public T obtener<T>() where T : BaseTablas, new()
        {
            T temp = new T();
            temp.Alumno = Alumno.Nombre;
            temp.Version = Alumno.UltimaVersion;
            temp.Programa = programaActual;
            return temp;
        }

        #endregion

        #region Obtener Datos

        public IEnumerable<SET> obtenerSET(ReplicaLectorDat.Datos datos)
        {
            return obtenerBaseSET(datos).Concat(obtenerReusedSET(datos)).Concat(obtenerNewSET(datos));
        }

        public IEnumerable<SET> obtenerBaseSET(ReplicaLectorDat.Datos datos)
        {
            if (!datos.ContainsKey("Base_Parts_List")) yield break;

            string[] temp = datos["Base_Parts_List"].ToString().Split('');
            foreach (var miau in temp)
            {
                int n;
                if (!int.TryParse(miau, out n))
                    continue;
                if (datos.checkData($"Base_Parts/{n}/Description") &&
                    datos.checkData($"Base_Parts/{n}/Base") &&
                    datos.checkData($"Base_Parts/{n}/Deleted") &&
                    datos.checkData($"Base_Parts/{n}/Modified") &&
                    datos.checkData($"Base_Parts/{n}/Added") &&
                    datos.checkData($"Base_Parts/{n}/Actual Base") &&
                    datos.checkData($"Base_Parts/{n}/Actual Deleted") &&
                    datos.checkData($"Base_Parts/{n}/Actual Modified") &&
                    datos.checkData($"Base_Parts/{n}/Actual Added"))
                    continue;
                SET baseSET = obtener<SET>();
                baseSET.TipoParte = "Base";
                baseSET.parse(x => x.Nombre, datos, $"Base_Parts/{n}/Description");
                baseSET.parse(x => x.EstBase, datos, $"Base_Parts/{n}/Base");
                baseSET.parse(x => x.EstDeleted, datos, $"Base_Parts/{n}/Deleted");
                baseSET.parse(x => x.EstModified, datos, $"Base_Parts/{n}/Modified");
                baseSET.parse(x => x.EstAdded, datos, $"Base_Parts/{n}/Added");
                baseSET.parse(x => x.ActBase, datos, $"Base_Parts/{n}/Actual Base");
                baseSET.parse(x => x.ActDeleted, datos, $"Base_Parts/{n}/Actual Deleted");
                baseSET.parse(x => x.ActModified, datos, $"Base_Parts/{n}/Actual Modified");
                baseSET.parse(x => x.ActAdded, datos, $"Base_Parts/{n}/Actual Added");
                baseSET.EstSize = baseSET.EstBase - baseSET.EstDeleted + baseSET.EstAdded;
                baseSET.ActSize = baseSET.ActBase - baseSET.ActDeleted + baseSET.ActAdded;

                yield return baseSET;
            }
        }

        public IEnumerable<SET> obtenerNewSET(ReplicaLectorDat.Datos datos)
        {
            if (!datos.ContainsKey("New_Objects_List")) yield break;

            string[] temp = datos["New_Objects_List"].Split('');
            foreach (var miau in temp)
            {
                int n;
                if (!int.TryParse(miau, out n))
                    continue;
                if (datos.checkData($"New Objects/{n}/Description") &&
                    datos.checkData($"New Objects/{n}/LOC") &&
                    datos.checkData($"New Objects/{n}/Type") &&
                    datos.checkData($"New Objects/{n}/Methods") &&
                    datos.checkData($"New Objects/{n}/Relative Size") &&
                    datos.checkData($"New Objects/{n}/New Reused") &&
                    datos.checkData($"New Objects/{n}/Actual LOC") &&
                    datos.checkData($"New Objects/{n}/Actual Methods") &&
                    datos.checkData($"New Objects/{n}/Actual New Reused"))
                    continue;
                SET newSET = obtener<SET>();
                newSET.TipoParte = "New";
                newSET.parse(x => x.Nombre, datos, $"New Objects/{n}/Description");
                newSET.parse(x => x.EstSize, datos, $"New Objects/{n}/LOC");
                newSET.parse(x => x.EstTipo, datos, $"New Objects/{n}/Type");
                newSET.parse(x => x.EstItems, datos, $"New Objects/{n}/Methods");
                newSET.parse(x => x.EstRelSize, datos, $"New Objects/{n}/Relative Size");
                newSET.parse(x => x.EstNR, datos, $"New Objects/{n}/New Reused");
                newSET.parse(x => x.ActSize, datos, $"New Objects/{n}/Actual LOC");
                newSET.parse(x => x.ActItems, datos, $"New Objects/{n}/Actual Methods");
                newSET.parse(x => x.ActNR, datos, $"New Objects/{n}/Actual New Reused");

                yield return newSET;
            }
        }

        public IEnumerable<SET> obtenerReusedSET(ReplicaLectorDat.Datos datos)
        {
            if (!datos.ContainsKey("Reused_Objects_List")) yield break;

            string[] temp = datos["Reused_Objects_List"].ToString().Split('');
            foreach (var miau in temp)
            {
                int n;
                if (!int.TryParse(miau, out n))
                    continue;
                if (datos.checkData($"Reused Objects/{n}/Description") &&
                    datos.checkData($"Reused Objects/{n}/LOC") &&
                    datos.checkData($"Reused Objects/{n}/Actual LOC"))
                    continue;
                SET reusedSET = obtener<SET>();
                reusedSET.TipoParte = "Resued";
                reusedSET.parse(x => x.Nombre, datos, $"Reused Objects/{n}/Description");
                reusedSET.parse(x => x.EstBase, datos, $"Reused Objects/{n}/LOC");
                reusedSET.parse(x => x.EstSize, datos, $"Reused Objects/{n}/LOC");
                reusedSET.parse(x => x.ActBase, datos, $"Reused Objects/{n}/Actual LOC");
                reusedSET.parse(x => x.ActSize, datos, $"Reused Objects/{n}/Actual LOC");

                yield return reusedSET;
            }
        }

        public PROBE obtenerPROBE(ReplicaLectorDat.Datos datos)
        {
            PROBE datosPROBE = obtener<PROBE>();
            datosPROBE.parse(x => x.TamProxy, datos, "PROBE_Last_Run_Value/Estimated Proxy Size");
            datosPROBE.parse(x => x.TamMetodo, datos, "Estimated New & Changed LOC/Probe Method");
            datosPROBE.parse(x => x.TamPlan, datos, "Estimated New & Changed LOC");
            datosPROBE.parse(x => x.TamR2, datos, "Estimated New & Changed LOC/R Squared");
            datosPROBE.parse(x => x.TamB0, datos, "Estimated New & Changed LOC/Beta0");
            datosPROBE.parse(x => x.TamB1, datos, "Estimated New & Changed LOC/Beta1");
            datosPROBE.parse(x => x.TamRango, datos, "Estimated New & Changed LOC/Range");
            datosPROBE.parse(x => x.TamUPI, datos, "Estimated New & Changed LOC/UPI");
            datosPROBE.parse(x => x.TamLPI, datos, "Estimated New & Changed LOC/LPI");
            datosPROBE.parse(x => x.TieMetodo, datos, "Estimated Time/Probe Method");
            datosPROBE.parse(x => x.TiePlan, datos, "Estimated Time");
            datosPROBE.parse(x => x.TieR2, datos, "Estimated Time/R Squared");
            datosPROBE.parse(x => x.TieB0, datos, "Estimated Time/Beta0");
            datosPROBE.parse(x => x.TieB1, datos, "Estimated Time/Beta1");
            datosPROBE.parse(x => x.TieRango, datos, "Estimated Time/Range");
            datosPROBE.parse(x => x.TieUPI, datos, "Estimated Time/UPI");
            datosPROBE.parse(x => x.TieLPI, datos, "Estimated Time/LPI");

            return datosPROBE;
        }

        public IEnumerable<BitacoraDefectos> obtenerBD(IEnumerable<LectorXML.Atributos> datosBD)
        {
            if (datosBD == null) yield break;

            foreach (var columnas in datosBD)
            {
                yield return obtenerBD(columnas);
            }
        }

        public BitacoraDefectos obtenerBD(LectorXML.Atributos datos)
        {
            BitacoraDefectos bd = obtener<BitacoraDefectos>();
            bd.parse(x => x.FaseInyectado, datos, "inj");
            bd.parse(x => x.FaseRemovido, datos, "rem");
            bd.parse(x => x.TipoDefecto, datos, "type");
            bd.parse(x => x.Descripcion, datos, "desc");
            bd.Fecha = defaultDate.AddMilliseconds(DataUtil.parse<double>(datos["date"].ToString().Trim().Substring(1))).ToLocalTime();
            bd.parse(x => x.IDDefecto, datos, "num");
            bd.parse(x => x.FixDefect, datos, "fd");
            bd.parse(x => x.FixCount, datos, "count");
            bd.parse(x => x.FixTime, datos, "ft");

            return bd;
        }

        public IEnumerable<BitacoraTiempo> obtenerBT(IEnumerable<LectorXML.Atributos> datosBT, Predicate<BitacoraTiempo> condicion)
        {
            //Agarra la información de los datos y lo guarda en la base de datos
            foreach (var columnas in datosBT)
            {
                var bt = obtenerBT(columnas);
                if (bt != null && condicion(bt))
                {
                    yield return bt;
                }
            }
        }

        public BitacoraTiempo obtenerBT(LectorXML.Atributos datos)
        {
            BitacoraTiempo bt = obtener<BitacoraTiempo>();
            if (!datos.ContainsKey("path") || datos["path"] == null) return null;
            string[] temp = datos["path"].Split('/');
            bt.Programa = temp[temp.Length - 2].Trim();
            bt.Fase = temp[temp.Length - 1].Trim();
            bt.parse(x => x.Interrupcion, datos, "interrupt");
            bt.parse(x => x.Delta, datos, "delta");
            bt.parse(x => bt.Comentario, datos, "comment");
            bt.Inicio = defaultDate.AddMilliseconds(DataUtil.parse<float>(datos["start"]?.Trim().Substring(1))).ToLocalTime();
            bt.Fin = bt.Inicio?.AddMinutes(bt.Delta + bt.Interrupcion);

            return bt;
        }

        private float totalLOCAFecha = 0;

        public IEnumerable<Fases> obtenerFases(ReplicaLectorDat.Datos datos, Dictionary<string, float> fasesTiempoAFecha, Dictionary<string, float> fasesInyectadasAFecha, PROBE probe)
        {
            float totalTiempoAFecha = 0, totalInyectadosAFecha = 0;
            foreach (var fase in ProcessDashboard.fases)
            {
                fasesTiempoAFecha[fase] += DataUtil.parse<float>(datos.Exists($"{fase}/Time"));
                fasesInyectadasAFecha[fase] += DataUtil.parse<float>(datos.Exists($"{fase}/Defects Injected"));
                totalTiempoAFecha += fasesTiempoAFecha[fase];
                totalInyectadosAFecha += fasesInyectadasAFecha[fase];
            }

            //ESTÁ MAL, SE SACA CON LA SUMA DE LOS ADDED Y MODIFIED
            totalLOCAFecha += DataUtil.parse<float>(datos.Exists("Total LOC"));
            
            float totalTiempo = DataUtil.parse<float>(datos.Exists("Estimated Time"));
            float totalInyectados = totalInyectadosAFecha * probe.TamPlan / totalLOCAFecha;

            foreach (var fase in ProcessDashboard.fases)
            {
                Fases temp = obtenerFases(datos, fase);
                if (temp == null) continue;
                temp.ResMin = fasesTiempoAFecha[fase] * totalTiempo / totalTiempoAFecha;
                temp.ResDefIny = fasesInyectadasAFecha[fase] * totalInyectados / totalInyectadosAFecha;
                yield return temp;
            }
        }

        public Fases obtenerFases(ReplicaLectorDat.Datos datos, string fase)
        {
            Fases fases = obtener<Fases>();
            fases.Fase = fase;
            if (datos.checkData($"{fase}/Time") &&
                    datos.checkData($"{fase}/Defects Injected") &&
                    datos.checkData($"{fase}/Defects Removed"))
                return null;
            fases.parse(x => x.EstMin, datos, $"{fase}/Time");
            fases.parse(x => x.EstDefIny, datos, $"{fase}/Defects Injected");
            fases.parse(x => x.EstDefRem, datos, $"{fase}/Defects Removed");
            return fases;
        }

        public IEnumerable<Resumen> obtenerResumen()
        {
            throw new NotImplementedException();
        }

        public Resumen obtenerResumen(string algo)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
