using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;

namespace ProcessDashboard
{
    public class LectorExcel
    {
        private string archivo;
        private Application xlApp;
        private Workbook xlWorkBook;
        private Worksheet xlWorkSheet = null;

        public LectorExcel(string archivo)
        {
            xlApp = new Application();
            if (File.Exists(archivo))
            {
                string checar = Path.GetExtension(archivo).Trim().ToLower();
                if (checar == ".xls" || checar == ".xlsx")
                {
                    object _missingValue = Missing.Value;
                    this.archivo = archivo;
                    xlWorkBook = xlApp.Workbooks.Open(this.archivo, _missingValue, false, _missingValue,
                        _missingValue, _missingValue, true, _missingValue, _missingValue,
                        true, _missingValue, _missingValue, _missingValue);
                }
            }
            else
            {
                throw new Exception($"El archivo {archivo} no existe");
            }
        }

        public void BuscarHoja(string Hoja)
        {
            Hoja = Hoja.Trim().ToLower();
            foreach (Worksheet tempHoja in xlWorkBook.Worksheets)
            {
                if (Hoja == tempHoja.Name.Trim().ToLower())
                {
                    xlWorkSheet = xlWorkBook.Worksheets[Hoja];
                    return;
                }
            }
            throw new Exception($"No se encontró la Hoja {Hoja}");
        }

        public void Map<T, M>(string columna, Func<T, M> p)
        {
        }

        public void LlenarDatos<T>(List<T> lista, Dictionary<string, Func<T, object>> columnas)
        {
            Dictionary<int, Func<T, object>> columnas2 = new Dictionary<int, Func<T, object>>();
            Range inicio = xlWorkSheet.Cells[1, 1];
            Range celda = xlWorkSheet.Range[inicio, inicio.End[XlDirection.xlToRight]];
            var la = celda.Value;
            for (int i = 1; i <= la.Length; i++)
            {
                string da = la[1, i]?.ToString();
                if (columnas.ContainsKey(da))
                {
                    columnas2.Add(i, columnas[da]);
                }
            }
            var contador = 2;
            foreach (T item in lista)
            {
                foreach (var kvp in columnas2)
                {
                    var da = kvp.Value(item);
                    xlWorkSheet.Cells[contador, kvp.Key].Value = da;
                }
                contador++;
            }

        }

        public void Guardar(string archivo = null)
        {
            if (archivo == null)
                archivo = this.archivo;
            if (!File.Exists(archivo))
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.RefreshAll();
                xlApp.Calculate();
                xlWorkBook.SaveCopyAs(archivo);
                xlWorkBook.Saved = true;
                xlApp.DisplayAlerts = true;
            }
            else
            {
                try
                {
                    xlWorkBook.SaveAs(archivo);
                    xlWorkBook.Saved = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("No se pudo guardar el archivo.");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Cerrar()
        {
            xlApp.Quit();
            releaseObject(xlApp);
            releaseObject(xlWorkBook);
            releaseObject(xlWorkSheet);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception e)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}