using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.Accessors.FileAccess {

    /// <summary>
    /// Class that Access a specified File inside the Zip and Reads its Information.
    /// </summary>
    /// <typeparam name="T">Type of Accessor to be used.</typeparam>
    /// <typeparam name="M">Type of Data to be Retrieved.</typeparam>
    /// <seealso cref="IReadableFileAccess{M}" />
    public abstract class FileAccess<T, M> : IReadableFileAccess<M> where T : Accessor where M : new() {
        #region Properties
        /// <summary>
        /// Accessor that Reads the File.
        /// </summary>
        public T accessor { get; protected set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAccess{T, M}"/> class.
        /// </summary>
        /// <param name="zip">The Zip Data.</param>
        /// <param name="directory">The Directory where ZipFile of the Student is.</param>
        /// <param name="fileName">The Name of the File inside Zip File that will be Accessed.</param>
        /// <param name="checkExtension">If set to <c>true</c> Checks the Extension of the File to ensure is correct.</param>
        public FileAccess(ZipFile zip, string directory, string fileName, bool checkExtension = true) {
            accessor = (T)Activator.CreateInstance(typeof(T), zip.getFile(directory, fileName), checkExtension);
        }

        /// <summary>
        /// Initializes the Accessor.
        /// </summary>
        /// <param name="accessor">The Accessor to be used.</param>
        public FileAccess(T accessor) {
            this.accessor = accessor;
        }
        #endregion

        #region Read
        /// <summary>
        /// Reads and Gets the Data from the File.
        /// </summary>
        /// <returns>Returns the Data Read.</returns>
        public abstract M InnerRead();

        /// <summary>
        /// Reads and Gets the Data from the File Safely.
        /// </summary>
        /// <returns>Returns the Data.</returns>
        public M Read() {
            try {
                return InnerRead();
            } catch {
                if (typeof(M) == typeof(Data)) {
                    return default(M);
                } else {
                    return new M();
                }
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes the File.
        /// </summary>
        public void Delete() {
            //Deletes the File
            File.Delete(accessor.file);
        }

        /// <summary>
        /// Releases the Access to the File by Deleting it.
        /// </summary>
        public void Dispose() {
            Delete();
        }
        #endregion
    }
}
