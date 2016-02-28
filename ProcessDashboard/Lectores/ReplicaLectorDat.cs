using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessDashboard.Lectores
{
    public class ReplicaLectorDat : Lector
    {
        private string[] texto;

        public Datos datos { get; private set; } = new Datos();

        public override string extension { get; protected set; } = ".dat";

        public ReplicaLectorDat(string archivo, bool checarExtension = false) : base(archivo, checarExtension) { }

        public void Leer(string sepDatos, string sepArreglos = null)
        {
            texto = File.ReadAllLines(this.archivo);
            foreach (string item in texto)
            {
                string[] temp = Regex.Split(item, sepDatos);
                if (temp.Length == 2)
                {
                    datos.Add(temp[0].Trim(), temp[1].Trim());
                }
            }
        }

        public void Guardar(string sepDatos, string sepArreglos = null)
        {
            if (texto == null) texto = File.ReadAllLines(archivo);

            for (int i = 0; i < texto.Length; i++)
            {
                string[] temp = Regex.Split(texto[i], sepDatos);
                temp[0] = temp[0].Trim();
                if (temp.Length == 2 && datos.ContainsKey(temp[0]))
                {
                    texto[i] = $"{temp[0]} = {datos[temp[0]]}";
                }
            }
            File.WriteAllLines(archivo, texto);
        }

        public string buscarDato(string sepDatos, string dato)
        {
            dato = dato.Trim().ToLower();
            foreach (string item in File.ReadLines(this.archivo))
            {
                string[] temp = Regex.Split(item, sepDatos);
                if (temp.Length == 2 && temp[0].Trim().ToLower().Equals(dato))
                {
                    return temp[1];
                }
            }
            return null;
        }

        public Dictionary<string, string> Leer(string sepDatos, HashSet<string> datos)
        {
            var tempDict = new Dictionary<string, string>();
            foreach (string item in File.ReadLines(this.archivo))
            {
                string[] temp = Regex.Split(item, sepDatos);
                if (temp.Length == 2 && datos.Contains(temp[0].Trim()))
                {
                    tempDict.Add(temp[0], temp[1]);
                }
            }
            return tempDict;
        }

        public class Datos : Dictionary<string, string>
        {

        }
    }
}
