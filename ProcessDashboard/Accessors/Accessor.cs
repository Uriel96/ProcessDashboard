using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors {

    /// <summary>
    /// Class in Charge of Reading and Modifing the File.
    /// </summary>
    public abstract class Accessor {
        /// <summary>
        /// The File that is used.
        /// </summary>
        public string file { get; protected set; }
        /// <summary>
        /// The Type of Extension that the File must be.
        /// </summary>
        public abstract string extension { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Accessor"/> class.
        /// </summary>
        /// <param name="file">The File that is used.</param>
        /// <param name="checkExtension">If set to <c>true</c> Checks the Extension of the File to ensure is correct.</param>
        /// <exception cref="Exception">
        /// The File Cannot be Read because it doesn't Exist or isn't the Correct Extension.
        /// </exception>
        public Accessor(string file, bool checkExtension = false) {
            if (!File.Exists(file))
                throw new Exception($"The file ({file}) you are trying to access does not exist.");
            if (checkExtension && !Path.GetExtension(file).ToLower().Equals(extension))
                throw new Exception($"The file ({file}) does not contain the correct extension ({extension}).");
            this.file = file;
        }
    }
}
