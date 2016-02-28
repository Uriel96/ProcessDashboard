using ProcessDashboard.DatosTemporales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public class ListedData : Linked
    {
        List<BitacoraDefectos> listaBD = new List<BitacoraDefectos>();
        List<BitacoraTiempo> listaBT = new List<BitacoraTiempo>();
        List<SET> listaSET = new List<SET>();
        List<PROBE> listaPROBE = new List<PROBE>();
        List<Fases> listaFases = new List<Fases>();
        List<Resumen> listaResumen = new List<Resumen>();

        public DatosAlumno da { get; set; }

        public void AgregarDatos(IEnumerable<BitacoraTiempo> datosBT = null, IEnumerable<BitacoraDefectos> datosBD = null, IEnumerable<SET> datosSET = null, PROBE datoPROBE = null, IEnumerable<Fases> datosFases = null, IEnumerable<Resumen> datosResumen = null)
        {
            listaBT?.AddRange(datosBT);
            listaBD?.AddRange(datosBD);
            listaSET?.AddRange(datosSET);
            listaPROBE?.Add(datoPROBE);
            listaFases?.AddRange(datosFases);
            listaResumen?.AddRange(datosResumen);
        }

        public void AgregarAlumno(string nombre)
        {
            //Crear un nuevo alumno
            da.Alumno = new Alumno() { Nombre = nombre, UltimaVersion = 0 };
        }

        public IEnumerable<BitacoraDefectos> obtenerBD() => listaBD.OrderBy(x => x.Programa).ThenBy(x => x.Fecha);

        public IEnumerable<BitacoraTiempo> obtenerBT() => listaBT.OrderBy(x => x.Programa).ThenBy(x => x.Inicio);
    
        public IEnumerable<SET> obtenerSET() => listaSET.OrderBy(x => x.Programa).ThenBy(x => x.TipoParte);

        public IEnumerable<PROBE> obtenerPROBE() => listaPROBE.OrderBy(x => x.Programa);

        public IEnumerable<Fases> obtenerFases() => listaFases.OrderBy(x => x.Programa).ThenBy(x => x.Fase);

        public IEnumerable<Resumen> obtenerResumen() => listaResumen.OrderBy(x => x.Programa);
    }
}
