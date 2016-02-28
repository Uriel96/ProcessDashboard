using Ionic.Zip;
using ProcessDashboard.DatosTemporales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard
{
    public static class DataUtil
    {
        public static bool checkData(this Dictionary<string, string> datos, string input)
        {
            if (!datos.ContainsKey(input)) return true;
            float dato = 0;
            Single.TryParse(datos[input], out dato);
            return datos[input] == "null" || datos[input] == "N/A" || dato == 0;
        }

        public static T parse<T>(string value)
        {
            T m = default(T);
            try
            {
                m = (T)Convert.ChangeType(value, m.GetType());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return m;
        }

        public static void parse<T, M>(this T outObj, Expression<Func<T, M>> outExpr, Dictionary<string, string> data, string input)
        {
            try
            {
                var expr = (MemberExpression)outExpr.Body;
                var prop = (PropertyInfo)expr.Member;
                Type type = Nullable.GetUnderlyingType(typeof(M)) ?? typeof(M);
                string temp;
                if (!data.TryGetValue(input, out temp)) throw new KeyNotFoundException($"No se pudo encontrar {input} en los datos");
                if (string.IsNullOrEmpty(temp) || temp.ToLower() == "null" || temp.ToLower() == "n/a") throw new NoNullAllowedException($"El valor de {input} no puede estar vacío");
                M value;
                if (type == typeof(int))
                    value = (M)Convert.ChangeType(Convert.ToDouble(temp), type);
                else
                    value = (M)Convert.ChangeType(temp, type);
                prop.SetValue(outObj, value, null);
            }
            catch (KeyNotFoundException e) { }
            catch (NoNullAllowedException e) { }
            catch (InvalidCastException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool obtenerArchivo(this ZipFile zip, string directory, string file)
        {
            if (!zip.ContainsEntry(file)) return false;
            zip[file].Extract(directory, ExtractExistingFileAction.OverwriteSilently);
            return true;
        }

        public static string buscarModificar(this string[] lista, Func<string, bool> comparar, Action<string> accion)
        {
            string temp = null;
            foreach (var item in lista)
            {
                accion(item);
                if (comparar(item))
                {
                    temp = item;
                }
            }
            return temp;
        }
        
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        public static T Exists<T>(this Dictionary<string, T> dict, string key) {
            return dict.ContainsKey(key) ? dict[key] : default(T);
        }
    }
}

