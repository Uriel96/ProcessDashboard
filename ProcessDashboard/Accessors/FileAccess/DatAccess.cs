using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors.FileAccess {
    /// <summary>
    /// Class that Access a .dat File and Retrieves the Data.
    /// </summary>
    /// <seealso cref="FileAccess{DatAccessor, Data}" />
    public class DatAccess : FileAccess<DatAccessor, Data> {

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAccess{T, M}"/> class.
        /// </summary>
        /// <param name="zip">The Zip Data.</param>
        /// <param name="directory">The Directory where ZipFile of the Student is.</param>
        /// <param name="fileName">The Name of the Dat File inside Zip File that will be Accessed.</param>
        public DatAccess(ZipFile zip, string directory, string fileName) : base(zip, directory, fileName, true) {

        }

        /// <summary>
        /// Reads and Gets the Data from the File.
        /// </summary>
        /// <returns>Returns the Data Read.</returns>
        public override Data InnerRead() {
            accessor.Read();
            return accessor.data;
        }
    }
}
