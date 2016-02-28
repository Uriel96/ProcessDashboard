using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProcessDashboard
{
    public class LectorXML : Lector
    {
        public override string extension { get; protected set; } = ".xml";

        public LectorXML(string archivo, bool checarExtension = false) : base(archivo, checarExtension) { }

        public IEnumerable<Atributos> Leer(string tag, params string[] atributos)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(this.archivo);
            }
            catch (XmlException e)
            {
                //throw new Exception("No se pudo leer el archivo XML de manera correcta.");
                Console.WriteLine("No se pudo leer el archivo XML de manera correcta.");
                yield break;
            }
            XmlNodeList listaDatos = xml.GetElementsByTagName(tag);
            foreach (XmlNode datos in listaDatos)
            {
                Atributos nuevosDatos = new Atributos();
                foreach (string atributo in atributos)
                {
                    string nuevoDato = datos.Attributes[atributo]?.Value ?? null;
                    nuevosDatos[atributo] = nuevoDato;
                }
                yield return nuevosDatos;
            }
        }

        public IEnumerable<Atributos> LeerDesde(string tag, string atributo = null, string valor = null, params string[] atributos)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(this.archivo);
            }
            catch (Exception)
            {
                throw new Exception("No se pudo leer el archivo XML de manera correcta.");
            }
            XmlNodeList listaDatos = xml.GetElementsByTagName(tag);
            XmlNode la = null;
            foreach (XmlNode datos in listaDatos)
            {
                XmlNode temp = datos.Attributes[atributo];
                if ((atributo != null && temp == null) || (atributo != null && temp != null && valor != null && temp.Value != valor))
                {
                    continue;
                }
                la = datos;
                break;

            }
            if (la == null)
            {
                throw new Exception("Not found");
            }
            XmlNodeList asd = la.ChildNodes;
            foreach (XmlNode datos in asd)
            {
                Atributos nuevosDatos = new Atributos();
                foreach (string atr in atributos)
                {
                    string nuevoDato = datos.Attributes?[atr]?.Value ?? null;
                    nuevosDatos[atr] = nuevoDato;
                }
                yield return nuevosDatos;
            }
        }

        public class Atributos : Dictionary<string, string>
        {

        }

    }
}
