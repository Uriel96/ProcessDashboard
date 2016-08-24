using Ionic.Zip;
using ProcessDashboard.Accessors;
using ProcessDashboard.Accessors.FileAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds a Student's Information
    /// </summary>
    public class Student : IDataConvertor {
        #region Properties        
        /// <summary>
        /// The Name of the Student (First and Last Name).
        /// </summary>
        [Key]
        public string Name { get; set; }
        /// <summary>
        /// The Last Generated Data Version of the Student.
        /// </summary>
        public int LastVersion { get; set; }
        #endregion
    }
}
