using ProcessDashboard.DatosTemporales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public interface Linked
    {
        DatosAlumno da { get; set; }

        void AgregarAlumno(string nombre);

        void AgregarDatos(IEnumerable<BitacoraTiempo> datosBT = null, IEnumerable<BitacoraDefectos> datosBD = null, IEnumerable<SET> datosSET = null, PROBE datoPROBE = null, IEnumerable<Fases> datosFases = null, IEnumerable<Resumen> datosResumen = null);

        IEnumerable<BitacoraDefectos> obtenerBD();

        IEnumerable<BitacoraTiempo> obtenerBT();

        IEnumerable<SET> obtenerSET();

        IEnumerable<PROBE> obtenerPROBE();

        IEnumerable<Fases> obtenerFases();

        IEnumerable<Resumen> obtenerResumen();
    }
}
