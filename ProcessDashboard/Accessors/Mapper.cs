using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors {
    /// <summary>
    /// Class that Holds the Columns of a Table and the Functions that returns the Information needed.
    /// </summary>
    /// <typeparam name="TInfo">Type of Information that Mapper uses.</typeparam>
    public class Mapper<TInfo> : Dictionary<string, Func<TInfo, dynamic>> where TInfo : IDataConvertor {

    }
}
