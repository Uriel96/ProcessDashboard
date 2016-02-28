using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public abstract class Lector
    {
        public string archivo { get; protected set; }
        public abstract string extension { get; protected set; }

        public Lector(string archivo, bool checarExtension = false)
        {
            if (!File.Exists(archivo))
                throw new Exception($"No se encontró el archivo que se trata de accesar: {archivo}");
            if (checarExtension && !archivo.ToLower().Trim().EndsWith(extension))
                throw new Exception($"El archivo seleccionado no contiene la extensión {extension}");
            this.archivo = archivo;
        }
    }
}
