using Microsoft.Office.Interop.Excel;
using ProcessDashboard.Accessors;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.Accessors {
    /// <summary>
    /// Class that Access an Excel File. Capable of Reading and Modifing it.
    /// </summary>
    public class ExcelAccessor : Accessor, IDisposable {
        #region Properties        
        /// <summary>
        /// The Excel Sheet that is Currently Accessing.
        /// </summary>
        public Worksheet sheet { get; private set; } = null;
        /*TODO: Add Regex to extension to implement ".xlsx|.xls"*/
        /// <summary>
        /// The Type of Extension that the File must be (.xlsx or .xls)
        /// </summary>
        public override string extension { get; protected set; } = ".xlsx";
        #endregion

        #region Fields        
        /// <summary>
        /// The Application used to Access the Excel File Information.
        /// </summary>
        private Application app;
        /// <summary>
        /// The Book of the Excel File where all Information is.
        /// </summary>
        private Workbook book;
        /// <summary>
        /// Stores the Amount of Data in each Sheet.
        /// </summary>
        private Dictionary<string, int> dataCounters = new Dictionary<string, int>();
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Information needed to Access an Excel File.
        /// </summary>
        /// <param name="file">The Excel File to be Accessed.</param>
        public ExcelAccessor(string file) : base(file, true) {
            app = new Application();
            object _missingValue = Missing.Value;
            this.file = file;
            book = app.Workbooks.Open(this.file, _missingValue, false, _missingValue,
                _missingValue, _missingValue, true, _missingValue, _missingValue,
                true, _missingValue, _missingValue, _missingValue);
            foreach (Worksheet sheet in book.Worksheets) {
                dataCounters.Add(sheet.Name.Trim().ToLower(), 0);
            }
        }
        #endregion

        #region Access        
        /// <summary>
        /// Searches and Sets the Specified Sheet.
        /// </summary>
        /// <param name="sheet">The Sheet to be Accessed.</param>
        /// <exception cref="Exception">Thrown when Sheet cannot be found.</exception>
        public void SearchSheet(string sheet) {
            sheet = sheet.Trim().ToLower();
            foreach (Worksheet temporalSheet in book.Worksheets) {
                if (sheet == temporalSheet.Name.Trim().ToLower()) {
                    this.sheet = book.Worksheets[sheet];
                    return;
                }
            }
            throw new Exception($"Could not find Sheet {sheet}");
        }
        #endregion

        #region Modify        
        /// <summary>
        /// Fills the list of Data in the Current Excel Sheet.
        /// </summary>
        /// <typeparam name="T">Type of Information of Information to be Added to the Excel File.</typeparam>
        /// <param name="list">The list of Information Processed.</param>
        /// <param name="mapper">The Mapper between the Columns and the Functions.</param>
        public void fill<T>(IEnumerable<T> list, Mapper<T> mapper) where T : IDataConvertor {
            var columns = new Dictionary<int, Func<T, dynamic>>();
            Range startCell = sheet.Cells[1, 1];
            Range columnsRage = sheet.Range[startCell, startCell.End[XlDirection.xlToRight]];
            //Get all the Columns
            var cells = columnsRage.Value;
            for (int i = 1; i <= cells.Length; i++) {
                string columnName = cells[1, i]?.ToString();
                if (mapper.ContainsKey(columnName)) {
                    columns.Add(i, mapper[columnName]);
                }
            }

            //Set all the data
            foreach (var item in list) {
                foreach (var kvp in columns) {
                    var value = kvp.Value(item);
                    sheet.Cells[getCurrentCounter() + 2, kvp.Key].Value = value;
                }
                incrementCurrentCounter();
            }
        }

        /// <summary>
        /// Fills the Data item in the Current Excel Sheet.
        /// </summary>
        /// <typeparam name="T">Type of Information of Information to be Added to the Excel File.</typeparam>
        /// <param name="item">The Information item Processed.</param>
        /// <param name="mapper">The Mapper between the Columns and the Functions.</param>
        public void fill<T>(T item, Mapper<T> mapper) where T : IDataConvertor {
            var columns = new Dictionary<int, Func<T, dynamic>>();
            Range startCell = sheet.Cells[1, 1];
            Range columnsRage = sheet.Range[startCell, startCell.End[XlDirection.xlToRight]];
            //Get all the Columns
            var cells = columnsRage.Value;
            for (int i = 1; i <= cells.Length; i++) {
                string columnName = cells[1, i]?.ToString();
                if (mapper.ContainsKey(columnName)) {
                    columns.Add(i, mapper[columnName]);
                }
            }

            //Set all the data
            foreach (var kvp in columns) {
                var value = kvp.Value(item);
                sheet.Cells[getCurrentCounter() + 2, kvp.Key].Value = value;
            }
            incrementCurrentCounter();
        }

        /// <summary>
        /// Adds a Column to the Sheet.
        /// </summary>
        /// <param name="column">The Name of the Column to be Added.</param>
        /// <param name="position">The Position where the Column will be Added.</param>
        public void AddColumn(string column, int position) {
            Range insetColumn = sheet.Cells[1, position];
            insetColumn.EntireColumn.Insert(XlInsertShiftDirection.xlShiftToRight,
                XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
            sheet.Cells[1, position].Value2 = column;
        }
        #endregion

        #region Save and Close        
        /// <summary>
        /// Saves the Excel File.
        /// </summary>
        /// <param name="file">The File where the new Excel will be Created. If not provided Saves it in the original file.</param>
        public void Save(string file = null) {
            //Sets AutoFit to every Sheet
            foreach (Worksheet sheet in book.Worksheets) {
                sheet.Columns.AutoFit();
            }
            if (file == null)
                file = this.file;
            if (!File.Exists(file)) {
                //Creates a new Excel if File doesn't exists
                app.DisplayAlerts = false;
                book.RefreshAll();
                app.Calculate();
                book.SaveCopyAs(file);
                book.Saved = true;
                app.DisplayAlerts = true;
            } else {
                //Saves the Excel File that used
                try {
                    book.SaveAs(file);
                    book.Saved = true;
                } catch (Exception e) {
                    Console.WriteLine("No se pudo guardar el archivo.");
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Closes the Excel Files and Releases its Information that is beeing used.
        /// </summary>
        public void Close() {
            object missingValue = Missing.Value;
            book.Close(false, missingValue, missingValue);
            app.Quit();
            releaseObject(app);
            releaseObject(book);
            releaseObject(sheet);
        }

        /// <summary>
        /// Closes the Excel Files and Releases its Information that is beeing used.
        /// </summary>
        public void Dispose() {
            Close();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Releases an specific object.
        /// </summary>
        /// <param name="obj">The object to be released.</param>
        private void releaseObject(object obj) {
            try {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            } catch {
                obj = null;
            } finally {
                GC.Collect();
            }
        }

        /// <summary>
        /// Gets the Counter of the Current Excel Sheet.
        /// </summary>
        /// <returns>Returns the Counter.</returns>
        public int getCurrentCounter() {
            return dataCounters[sheet.Name.Trim().ToLower()];
        }

        /// <summary>
        /// Increments the Counter of the Current Excel Sheet.
        /// </summary>
        /// <returns>Returns the Incremented Counter.</returns>
        private int incrementCurrentCounter() {
            return dataCounters[sheet.Name.Trim().ToLower()]++;
        }
        #endregion
    }
}