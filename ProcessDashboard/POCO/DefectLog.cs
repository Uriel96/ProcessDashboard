using ProcessDashboard.Accessors;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds the Defect Logs Information.
    /// </summary>
    public class DefectLog : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the Defect Logs Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /*TODO: Check if Description is Correct*/
        /// <summary>
        /// The Defect ID Solving when Defect was Injected.
        /// </summary>
        public int? DefectID { get; set; }
        /// <summary>
        /// The Date when the Defect was found.
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// The Phase when the Defect was Injected.
        /// </summary>
        public string PhaseInjected { get; set; }
        /// <summary>
        /// The Phase when the Defect was Removed.
        /// </summary>
        public string PhaseRemoved { get; set; }
        /// <summary>
        /// The Type of Defect found (Documentation, Syntax, Build, Assignment, Interface, Checking, Data, Function, System, Environment).
        /// </summary>
        public string DefectType { get; set; }
        /// <summary>
        /// The Time it took to Fix the Defect.
        /// </summary>
        public float FixTime { get; set; }
        /// <summary>
        /// The Number of Similar Defects Fixed.
        /// </summary>
        public int FixCount { get; set; } = 1;
        /*TODO: Check if Description is Correct*/
        /// <summary>
        /// The Defect Solving when Defect was Injected.
        /// </summary>
        public int? FixDefect { get; set; }
        /// <summary>
        /// The Description for the Defect found.
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Fields        
        /// <summary>
        /// The data that will be taken.
        /// </summary>
        [NotMapped]
        private Data data;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The data.</param>
        public void initialize(Data data) {
            this.data = data;
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            setPhaseInjected();
            setPhaseRemoved();
            setDefectType();
            setDescription();
            setDate();
            setDefectID();
            setFixDefect();
            setFixCount();
            setFixTime();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the phase injected.
        /// </summary>
        public void setPhaseInjected() {
            this.parse(x => x.PhaseInjected, data, "inj");
        }
        /// <summary>
        /// Sets the phase removed.
        /// </summary>
        public void setPhaseRemoved() {
            this.parse(x => x.PhaseRemoved, data, "rem");
        }
        /// <summary>
        /// Sets the type of the defect.
        /// </summary>
        public void setDefectType() {
            this.parse(x => x.DefectType, data, "type");
        }
        /// <summary>
        /// Sets the description.
        /// </summary>
        public void setDescription() {
            this.parse(x => x.Description, data, "desc");
        }
        /// <summary>
        /// Sets the date.
        /// </summary>
        public void setDate() {
            Date = defaultDate.AddMilliseconds(data["date"].ToString().Trim().Substring(1).parse<double>()).ToLocalTime();
        }
        /// <summary>
        /// Sets the defect ID.
        /// </summary>
        public void setDefectID() {
            this.parse(x => x.DefectID, data, "num");
        }
        /// <summary>
        /// Sets the fix defect.
        /// </summary>
        public void setFixDefect() {
            this.parse(x => x.FixDefect, data, "fd");
        }
        /// <summary>
        /// Sets the fix count.
        /// </summary>
        public void setFixCount() {
            this.parse(x => x.FixCount, data, "count");
        }
        /// <summary>
        /// Sets the fix time.
        /// </summary>
        public void setFixTime() {
            this.parse(x => x.FixTime, data, "ft");
        }
        #endregion
    }
}
