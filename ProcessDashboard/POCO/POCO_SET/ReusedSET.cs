using ProcessDashboard.Accessors;
using ProcessDashboard.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.POCO_SET {
    /// <summary>
    /// Class that holds the Reused Size Estimating Template Information.
    /// </summary>
    public class ReusedSET : SET {
        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="index">The index of the Current Part.</param>
        public override void initialize(Data data, int index) {
            initialize(data, SETType.Reused, index);
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            if (!pass())
                return;
            setName();
            setEstBase();
            setEstSize();
            setActBase();
            setActSize();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the Name.
        /// </summary>
        public void setName() {
            this.parse(x => x.Name, data, $"Reused Objects/{index}/Description");
        }
        /// <summary>
        /// Sets the Estimated Base.
        /// </summary>
        public void setEstBase() {
            this.parse(x => x.EstBase, data, $"Reused Objects/{index}/LOC");
        }
        /// <summary>
        /// Sets the Estimated Size.
        /// </summary>
        public void setEstSize() {
            this.parse(x => x.EstSize, data, $"Reused Objects/{index}/LOC");
        }
        /// <summary>
        /// Sets the Actual Base.
        /// </summary>
        public void setActBase() {
            this.parse(x => x.ActBase, data, $"Reused Objects/{index}/Actual LOC");
        }
        /// <summary>
        /// Sets the Actual Size.
        /// </summary>
        public void setActSize() {
            this.parse(x => x.ActSize, data, $"Reused Objects/{index}/Actual LOC");
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
                $"Reused Objects/{index}/Description",
                $"Reused Objects/{index}/LOC",
                $"Reused Objects/{index}/Actual LOC");
        }
        #endregion
    }
}
