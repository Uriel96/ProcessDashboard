using ProcessDashboard.Accessors;
using ProcessDashboard.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.POCO_SET {
    /// <summary>
    /// Class that holds the Base Size Estimating Template Information.
    /// </summary>
    public class BaseSET : SET {
        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="index">The index of the Current Part.</param>
        public override void initialize(Data data, int index) {
            initialize(data, SETType.Base, index);
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Base SET Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            if (!pass())
                return;
            setName();
            setEstBase();
            setEstDeleted();
            setEstModified();
            setEstAdded();
            setActBase();
            setActDeleted();
            setActModified();
            setActAdded();
            setEstSize();
            setActSize();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the Name.
        /// </summary>
        public void setName() {
            this.parse(x => x.Name, data, $"Base_Parts/{index}/Description");
        }
        /// <summary>
        /// Sets the Estimated Base.
        /// </summary>
        public void setEstBase() {
            this.parse(x => x.EstBase, data, $"Base_Parts/{index}/Base");
        }
        /// <summary>
        /// Sets the Estimated Deleted.
        /// </summary>
        public void setEstDeleted() {
            this.parse(x => x.EstDeleted, data, $"Base_Parts/{index}/Deleted");
        }
        /// <summary>
        /// Sets the Estimated Modified.
        /// </summary>
        public void setEstModified() {
            this.parse(x => x.EstModified, data, $"Base_Parts/{index}/Modified");
        }
        /// <summary>
        /// Sets the Estimated Added.
        /// </summary>
        public void setEstAdded() {
            this.parse(x => x.EstAdded, data, $"Base_Parts/{index}/Added");
        }
        /// <summary>
        /// Sets the Size Estimated.
        /// </summary>
        public void setEstSize() {
            EstSize = EstBase - EstDeleted + EstAdded;
        }

        /// <summary>
        /// Sets the Actual Base.
        /// </summary>
        public void setActBase() {
            this.parse(x => x.ActBase, data, $"Base_Parts/{index}/Actual Base");
        }
        /// <summary>
        /// Sets the Actual Deleted.
        /// </summary>
        public void setActDeleted() {
            this.parse(x => x.ActDeleted, data, $"Base_Parts/{index}/Actual Deleted");
        }
        /// <summary>
        /// Sets the Actual Modified.
        /// </summary>
        public void setActModified() {
            this.parse(x => x.ActModified, data, $"Base_Parts/{index}/Actual Modified");
        }
        /// <summary>
        /// Sets the Actual Added.
        /// </summary>
        public void setActAdded() {
            this.parse(x => x.ActAdded, data, $"Base_Parts/{index}/Actual Added");
        }
        /// <summary>
        /// Sets Actual Size.
        /// </summary>
        public void setActSize() {
            ActSize = ActBase - ActDeleted + ActAdded;
        }
        #endregion

        #region Helper Methods        
        /// <summary>
        /// Checks if the necesary Information is in the Data.
        /// </summary>
        /// <returns>
        /// Returns whether or not the necesary Information is in the Data.
        /// </returns>
        public override bool pass() {
            return data.exists(
                $"Base_Parts/{index}/Description",
                $"Base_Parts/{index}/Base",
                $"Base_Parts/{index}/Deleted",
                $"Base_Parts/{index}/Modified",
                $"Base_Parts/{index}/Added",
                $"Base_Parts/{index}/Actual Base",
                $"Base_Parts/{index}/Actual Deleted",
                $"Base_Parts/{index}/Actual Modified",
                $"Base_Parts/{index}/Actual Added");
        }
        #endregion
    }
}
