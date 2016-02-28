using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ProcessDashboard.DatosTemporales
{
    public abstract class BaseTablas
    {
        [Key]
        public int ID { get; set; }

        public string Alumno { get; set; }

        public string Programa { get; set; }
        
        public int Version { get; set; }

        public override string ToString()
        {
            string resultado = "";
            int cont = 0;
            var cantidadPropiedades = GetType().GetProperties().Length;
            PropertyInfo[] propBase = GetType().BaseType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] propDerivadas = GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < propBase.Length; i++, cont++) {
                string coma = cont < cantidadPropiedades - 1 ? ", " : "";
                resultado += $"{ propBase[i].Name }: { propBase[i].GetValue(this) }{ coma }";
            }
            for (int i = 0; i < propDerivadas.Length; i++, cont++) {
                string coma = cont < cantidadPropiedades - 1 ? ", " : "";
                resultado += $"{ propDerivadas[i].Name }: { propDerivadas[i].GetValue(this) }{ coma }";
            }
            return resultado;
        }
    }
}
