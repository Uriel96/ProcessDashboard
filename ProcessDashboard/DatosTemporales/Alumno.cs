using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.DatosTemporales
{
    public class Alumno
    {
        [Key]
        public string Nombre { get; set; }
        public int UltimaVersion { get; set; }
    }
}
