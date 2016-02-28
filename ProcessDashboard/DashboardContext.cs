using Microsoft.Data.Entity;
using Microsoft.Data.Sqlite;
using ProcessDashboard.DatosTemporales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public class DashboardContext : DbContext, Linked
    {
        public DbSet<Alumno> tablaAlumnos { get; set; }
        public DbSet<BitacoraDefectos> tablaBD { get; set; }
        public DbSet<BitacoraTiempo> tablaBT { get; set; }
        public DbSet<SET> tablaSET { get; set; }
        public DbSet<PROBE> tablaPROBE { get; set; }
        public DbSet<Fases> tablaFases { get; set; }
        public DbSet<Resumen> tablaResumen { get; set; }

        public DatosAlumno da { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "dashboard.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }

        public void AgregarAlumno(string nombre)
        {
            if (tablaAlumnos.Any(x => x.Nombre.Equals(nombre)))
            {
                //Buscar el ID y la Versión del Alumno
                da.Alumno = tablaAlumnos.FirstOrDefault(x => x.Nombre.Equals(nombre));
                da.Alumno.UltimaVersion++;
                //Limpiar las versiones anteriores
                tablaBD.RemoveRange(tablaBD.Where(x => x.Alumno.Equals(da.Alumno.Nombre) && x.Version < da.Alumno.UltimaVersion - 1));
                tablaBT.RemoveRange(tablaBT.Where(x => x.Alumno.Equals(da.Alumno.Nombre) && x.Version < da.Alumno.UltimaVersion - 1));
                tablaSET.RemoveRange(tablaSET.Where(x => x.Alumno.Equals(da.Alumno.Nombre) && x.Version < da.Alumno.UltimaVersion - 1));
                tablaPROBE.RemoveRange(tablaPROBE.Where(x => x.Alumno.Equals(da.Alumno.Nombre) && x.Version < da.Alumno.UltimaVersion - 1));
            }
            else
            {
                da.Alumno = new Alumno() { Nombre = nombre, UltimaVersion = 0 };
                //Agregarlo a la base de datos
                tablaAlumnos.Add(da.Alumno);
            }
        }

        public void AgregarDatos(IEnumerable<BitacoraTiempo> datosBT = null, IEnumerable<BitacoraDefectos> datosBD = null, IEnumerable<SET> datosSET = null, PROBE datoPROBE = null, IEnumerable<Fases> datosFases = null, IEnumerable<Resumen> datosResumen = null)
        {
            foreach (var dato in datosBT.OrEmpty()) tablaBT.Add(dato);
            foreach (var dato in datosBD.OrEmpty()) tablaBD.Add(dato);
            foreach (var dato in datosSET.OrEmpty()) tablaSET.Add(dato);
            if (datoPROBE != null) tablaPROBE.Add(datoPROBE);
            foreach (var dato in datosFases.OrEmpty()) tablaFases.Add(dato);
            foreach (var dato in datosResumen.OrEmpty()) tablaResumen.Add(dato);
        }

        public IEnumerable<BitacoraDefectos> obtenerBD() =>
            obtenerDatos(tablaBD).OrderBy(x => x.Programa).ThenBy(x => x.Fecha);

        public IEnumerable<BitacoraTiempo> obtenerBT() =>
            obtenerDatos(tablaBT).OrderBy(x => x.Programa).ThenBy(x => x.Inicio);
      
        public IEnumerable<SET> obtenerSET() =>
            obtenerDatos(tablaSET).OrderBy(x => x.Programa).ThenBy(x => x.TipoParte);

        public IEnumerable<PROBE> obtenerPROBE() =>
            obtenerDatos(tablaPROBE).OrderBy(x => x.Programa);

        public IEnumerable<Fases> obtenerFases() =>
            obtenerDatos(tablaFases).OrderBy(x => x.Programa).ThenBy(x => x.Fase);

        public IEnumerable<Resumen> obtenerResumen() =>
            obtenerDatos(tablaResumen).OrderBy(x => x.Programa);

        public IEnumerable<T> obtenerDatos<T>(DbSet<T> tabla) where T : BaseTablas =>
            tabla.Where(x => x.Alumno.Equals(da.Alumno.Nombre) && x.Version == da.Alumno.UltimaVersion);
    }
}
