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
    /// Class in Charge of Accessing Information from the State File.
    /// </summary>
    /// <seealso cref="FileAccess{XMLAccessor, Data}" />
    public class StateAccess : FileAccess<XMLAccessor, List<Data>> {

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAccess"/> class.
        /// </summary>
        /// <param name="zip">The Zip Data.</param>
        /// <param name="directory">The Directory where ZipFile of the Student is.</param>
        /// <param name="fileName">The Name of the File inside Zip File that will be Accessed.</param>
        public StateAccess(ZipFile zip, string directory, string fileName) : base(zip, directory, fileName, false) {

        }

        /// <summary>
        /// Reads the Data of the State File.
        /// </summary>
        /// <returns>Returns the List of Programs Information.</returns>
        public override List<Data> InnerRead() {
            accessor.Read("node", node => node.Attributes["name"]?.Value == "Student Profile", "name", "dataFile", "defectLog");
            return accessor.data;
        }
    }
}
