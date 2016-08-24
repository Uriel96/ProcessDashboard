using ProcessDashboard.Accessors;
using ProcessDashboard.HelperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO.POCO_SET {
    /// <summary>
    /// The Type of Size Estimate Template Part.
    /// </summary>
    public enum SETType {
        /// <summary>
        /// The Base Part where the Code is used.
        /// </summary>
        Base,
        /// <summary>
        /// The New Part where Code is generated.
        /// </summary>
        New,
        /// <summary>
        /// The Reused Part where Code is used again.
        /// </summary>
        Reused
    };

    /// <summary>
    /// Class that holds the Size Estimating Template Information.
    /// </summary>
    public abstract class SET : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the SET Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /// <summary>
        /// The type of SET Information is holding (Base, New or Reused).
        /// </summary>
        public SETType Type { get; set; }
        /// <summary>
        /// The Name of the Size Part.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Estimated Base Lines of Code.
        /// </summary>
        public float EstBase { get; set; }
        /// <summary>
        /// The Estimated Deleted Lines of Code.
        /// </summary>
        public float EstDeleted { get; set; }
        /// <summary>
        /// The Estimated Modified Lines of Code.
        /// </summary>
        public float EstModified { get; set; }
        /// <summary>
        /// The Estimated Added Lines of Code.
        /// </summary>
        public float EstAdded { get; set; }
        /// <summary>
        /// The Estimated Total Lines of Code.
        /// </summary>
        public float EstSize { get; set; }
        /// <summary>
        /// The Estimated Type of Added Part (only for New SET).
        /// </summary>
        public string EstType { get; set; }
        /// <summary>
        /// The Estimated number of Methods that will be Added (only for New SET).
        /// </summary>
        public int EstItems { get; set; }
        /// <summary>
        /// The Estimated Relative Size (very small, small, medium, large, very large) that will be Added (only for New SET).
        /// </summary>
        public string EstRelSize { get; set; }
        /*TODO: Save as boolean*/
        /// <summary>
        /// If Estimates that Code will be Reused later.
        /// </summary>
        public int EstNR { get; set; }

        /// <summary>
        /// The Actual Base Lines of Code.
        /// </summary>
        public float ActBase { get; set; }
        /// <summary>
        /// The Actual Deleted Lines of Code.
        /// </summary>
        public float ActDeleted { get; set; }
        /// <summary>
        /// The Actual Modified Lines of Code.
        /// </summary>
        public float ActModified { get; set; }
        /// <summary>
        /// The Actual Added Lines of Code.
        /// </summary>
        public float ActAdded { get; set; }
        /// <summary>
        /// The Actual Total Lines of Code.
        /// </summary>
        public float ActSize { get; set; }
        /// <summary>
        /// The Actual number of Methods that will be Added (only for New SET).
        /// </summary>
        public int ActItems { get; set; }
        /*TODO: Save as boolean*/
        /// <summary>
        /// If Actual that Code will be Reused later.
        /// </summary>
        public int ActNR { get; set; }
        #endregion

        #region Fields        
        /// <summary>
        /// The data that will be taken.
        /// </summary>
        [NotMapped]
        protected Data data;
        /// <summary>
        /// The Index of the Current Part Data.
        /// </summary>
        [NotMapped]
        protected int index;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="Type">The type of SET.</param>
        /// <param name="index">The index of the Current Part.</param>
        protected void initialize(Data data, SETType Type, int index) {
            this.data = data;
            this.Type = Type;
            this.index = index;
        }

        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The Data.</param>
        /// <param name="index">The index of the Current Part.</param>
        public abstract void initialize(Data data, int index);
        #endregion

        #region Helper Methods        
        /// <summary>
        /// Checks if the necesary Information is in the Data.
        /// </summary>
        /// <returns>Returns whether or not the necesary Information is in the Data.</returns>
        public abstract bool pass();
        #endregion
    }
}
