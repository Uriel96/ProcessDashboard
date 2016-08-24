using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors {
    /// <summary>
    /// Class that Access Information from a Txt File.
    /// </summary>
    public class TxtAccessor : Accessor {
        #region Properties
        /// <summary>
        /// The Separator between the Column and the Value (as a Regex).
        /// </summary>
        public virtual string dataSeparator { get; set; } = "=";
        /// <summary>
        /// The Separator between the Array Values.
        /// </summary>
        public virtual string arraySeparator { get; set; } = "|";
        /// <summary>
        /// The Extension that the File Accessed must have.
        /// </summary>
        public override string extension { get; protected set; } = ".txt";
        /// <summary>
        /// The Data Accessed from the File when Read.
        /// </summary>
        public Data data { get; protected set; } = new Data();
        #endregion

        #region Fields        
        /// <summary>
        /// Stores all the lines the Txt File Cointains.
        /// </summary>
        private string[] lines;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TxtAccessor"/> class.
        /// </summary>
        /// <param name="file">The File that is used.</param>
        /// <param name="checkExtension">If set to <c>true</c> Checks the Extension of the File to ensure is correct.</param>
        public TxtAccessor(string file, bool checkExtension = false) : base(file, checkExtension) { }
        #endregion

        #region Read
        /// <summary>
        /// Reads the Information from the Txt File and Stores it in Data.
        /// </summary>
        public void Read() {
            if (lines == null)
                lines = File.ReadAllLines(file);
            foreach (var line in lines) {
                string[] pair = Regex.Split(line, dataSeparator);
                if (pair.Length == 2) {
                    data.Add(pair[0].Trim(), pair[1].Trim());
                }
            }
        }

        /// <summary>
        /// Searches a Specific Value in the File.
        /// </summary>
        /// <param name="input">The Column to be found in the File.</param>
        /// <returns>Returns the Value found.</returns>
        public string searchValue(string input) {
            input = input.Trim().ToLower();
            if (lines == null)
                lines = File.ReadAllLines(file);
            foreach (var line in lines) {
                string[] pair = Regex.Split(line, dataSeparator);
                if (pair.Length >= 2 && pair[0].Trim().ToLower().Equals(input)) {
                    /*TODO: Set data to separate only the Column.*/
                    return pair[1];
                }
            }
            return null;
        }
        #endregion

        #region Modify
        /// <summary>
        /// Saves the File with the all the Modifications made.
        /// </summary>
        public void Save() {
            var newLines = new List<string>();
            foreach (var kvp in data) {
                newLines.Add($"{kvp.Key} {dataSeparator} {kvp.Value}");
            }
            File.WriteAllLines(file, newLines);
        }
        #endregion
    }
}
