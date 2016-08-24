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
    /// Class that holds the New Size Estimating Template Information.
    /// </summary>
    public class NewSET : SET {
        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="index">The index of the Current Part.</param>
        public override void initialize(Data data, int index) {
            initialize(data, SETType.New, index);
        }
        #endregion

        #region Main Methods
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            if (!pass())
                return;
            setDescription();
            setEstSize();
            setEstType();
            setEstItems();
            setEstRelSize();
            setEstNR();
            setActSize();
            setActItems();
            setActNR();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the Description.
        /// </summary>
        public void setDescription() {
            this.parse(x => x.Name, data, $"New Objects/{index}/Description");
        }
        /// <summary>
        /// Sets the Estimated Size.
        /// </summary>
        public void setEstSize() {
            this.parse(x => x.EstSize, data, $"New Objects/{index}/LOC");
        }
        /// <summary>
        /// Sets the Estimated Type.
        /// </summary>
        public void setEstType() {
            this.parse(x => x.EstType, data, $"New Objects/{index}/Type");
        }
        /// <summary>
        /// Sets the Esimated Items.
        /// </summary>
        public void setEstItems() {
            this.parse(x => x.EstItems, data, $"New Objects/{index}/Methods");
        }
        /// <summary>
        /// Sets the Estimated Relative Size.
        /// </summary>
        public void setEstRelSize() {
            this.parse(x => x.EstRelSize, data, $"New Objects/{index}/Relative Size");
        }
        /// <summary>
        /// Sets the Estimated New Reused.
        /// </summary>
        public void setEstNR() {
            this.parse(x => x.EstNR, data, $"New Objects/{index}/New Reused");
        }

        /// <summary>
        /// Sets the Actual Size.
        /// </summary>
        public void setActSize() {
            this.parse(x => x.ActSize, data, $"New Objects/{index}/Actual LOC");
        }
        /// <summary>
        /// Sets the Actual Items.
        /// </summary>
        public void setActItems() {
            this.parse(x => x.ActItems, data, $"New Objects/{index}/Actual Methods");
        }
        /// <summary>
        /// Sets the Actual New Reused.
        /// </summary>
        public void setActNR() {
            this.parse(x => x.ActNR, data, $"New Objects/{index}/Actual New Reused");
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
                $"New Objects/{index}/Description",
                $"New Objects/{index}/LOC",
                $"New Objects/{index}/Type",
                $"New Objects/{index}/Methods",
                $"New Objects/{index}/Relative Size",
                $"New Objects/{index}/New Reused",
                $"New Objects/{index}/Actual LOC",
                $"New Objects/{index}/Actual Methods",
                $"New Objects/{index}/Actual New Reused");
        }
        #endregion
    }
}
