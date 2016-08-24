using Ionic.Zip;
using ProcessDashboard.Accessors;
using ProcessDashboard.Accessors.FileAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors {
    /// <summary>
    /// Class that Access Information from a Dat File.
    /// </summary>
    public class DatAccessor : TxtAccessor {
        #region Properties
        /// <summary>
        /// The Separator between the Column and the Value (as a Regex).
        /// </summary>
        public override string dataSeparator { get; set; } = "==#|\\?=|=@|=\"|==|=";
        /// <summary>
        /// The Extension that the File Accessed must have.
        /// </summary>
        public override string extension { get; protected set; } = ".dat";
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DatAccessor"/> class.
        /// </summary>
        /// <param name="file">The File to Access.</param>
        /// <param name="checkExtension">if set to <c>true</c> checks the extension of the File to be .dat.</param>
        public DatAccessor(string file, bool checkExtension) : base(file, checkExtension) {

        }
        #endregion
    }
}
