using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.Accessors.FileAccess {
    /// <summary>
    /// Interface that holds Control of Reading Data.
    /// </summary>
    /// <typeparam name="TData">Type of Data to be Read.</typeparam>
    /// <seealso cref="IDisposable" />
    public interface IReadableFileAccess<TData> : IDisposable {
        /// <summary>
        /// Reads and Gets the data.
        /// </summary>
        /// <returns>Returns the Data.</returns>
        TData Read();
    }
}
