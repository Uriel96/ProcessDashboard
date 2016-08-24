using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors.FileAccess {
    /// <summary>
    /// Class in Charge of Accessing Information from the Time Log File.
    /// </summary>
    /// <seealso cref="FileAccess{XMLAccessor, Data}" />
    public class TimeLogAccess : FileAccess<XMLAccessor, List<Data>> {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeLogAccess"/> class.
        /// </summary>
        /// <param name="zip">The Zip Data.</param>
        /// <param name="directory">The Directory where ZipFile of the Student is.</param>
        /// <param name="fileName">The Name of the Time Log File inside Zip File that will be Accessed.</param>
        public TimeLogAccess(ZipFile zip, string directory, string fileName) : base(zip, directory, fileName, false) {

        }

        /// <summary>
        /// Reads and Gets the List of Defects from the XML File.
        /// </summary>
        /// <returns>
        /// Returns the List of Defects.
        /// </returns>
        public override List<Data> InnerRead() {
            accessor.Read("time", "id", "path", "start", "delta", "interrupt", "comment");
            return accessor.data;
        }
    }
}
