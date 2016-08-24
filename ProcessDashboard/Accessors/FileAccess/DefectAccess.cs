using Ionic.Zip;
using ProcessDashboard.Accessors.FileAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors.FileAccess {
    /// <summary>
    /// Class that Access the Defect XML File.
    /// </summary>
    /// <seealso cref="FileAccess{XMLAccessor, Data}" />
    class DefectAccess : FileAccess<XMLAccessor, List<Data>> {

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAccess{T, M}"/> class.
        /// </summary>
        /// <param name="zip">The Zip Data.</param>
        /// <param name="directory">The Directory where ZipFile of the Student is.</param>
        /// <param name="fileName">The Name of the Defect File inside Zip File that will be Accessed.</param>
        public DefectAccess(ZipFile zip, string directory, string fileName) : base(zip, directory, fileName, false) {

        }

        /// <summary>
        /// Reads the File and Gets the Defects from the XML File.
        /// </summary>
        /// <returns>Returns the List of Defects.</returns>
        public override List<Data> InnerRead() {
            accessor.Read("defect", "num", "date", "inj", "rem", "type", "ft", "count", "fd", "desc");
            return accessor.data;
        }
    }
}
