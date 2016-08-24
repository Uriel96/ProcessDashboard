using ProcessDashboard.Accessors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessDashboard.HelperClasses.DataUtil;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds the Proxy Based Estimation Method Information.
    /// </summary>
    public class PROBE : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the PROBE Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /// <summary>
        /// The Plan Size.
        /// </summary>
        public float SizeProxy { get; set; }
        /// <summary>
        /// The PROBE Method used to Estimate the Size.
        /// </summary>
        public string SizeMethod { get; set; }
        /// <summary>
        /// The Estimated Size.
        /// </summary>
        public float SizePlan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeR2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeB0 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeB1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeRange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeUPI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float SizeLPI { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TimeMethod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimePlan { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeR2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeB0 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeB1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeRange { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeUPI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TimeLPI { get; set; }
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
            setSizeProxy();
            setSizeMethod();
            setSizePlan();
            setSizeR2();
            setSizeB0();
            setSizeB1();
            setSizeRange();
            setSizeUPI();
            setSizeLPI();
            setTimeMethod();
            setTimePlan();
            setTimeR2();
            setTimeB0();
            setTimeB1();
            setTimeRange();
            setTimeUPI();
            setTimeLPI();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the size proxy.
        /// </summary>
        public void setSizeProxy() {
            this.parse(x => x.SizeProxy, data, "PROBE_Last_Run_Value/Estimated Proxy Size");
        }
        /// <summary>
        /// Sets the size method.
        /// </summary>
        public void setSizeMethod() {
            this.parse(x => x.SizeMethod, data, "Estimated New & Changed LOC/Probe Method");
        }
        /// <summary>
        /// Sets the size plan.
        /// </summary>
        public void setSizePlan() {
            this.parse(x => x.SizePlan, data, "Estimated New & Changed LOC");
        }
        /// <summary>
        /// Sets the size R2.
        /// </summary>
        public void setSizeR2() {
            this.parse(x => x.SizeR2, data, "Estimated New & Changed LOC/R Squared");
        }
        /// <summary>
        /// Sets the size B0.
        /// </summary>
        public void setSizeB0() {
            this.parse(x => x.SizeB0, data, "Estimated New & Changed LOC/Beta0");
        }
        /// <summary>
        /// Sets the size B1.
        /// </summary>
        public void setSizeB1() {
            this.parse(x => x.SizeB1, data, "Estimated New & Changed LOC/Beta1");
        }
        /// <summary>
        /// Sets the size range.
        /// </summary>
        public void setSizeRange() {
            this.parse(x => x.SizeRange, data, "Estimated New & Changed LOC/Range");
        }
        /// <summary>
        /// Sets the size UPI.
        /// </summary>
        public void setSizeUPI() {
            this.parse(x => x.SizeUPI, data, "Estimated New & Changed LOC/UPI");
        }
        /// <summary>
        /// Sets the size LPI.
        /// </summary>
        public void setSizeLPI() {
            this.parse(x => x.SizeLPI, data, "Estimated New & Changed LOC/LPI");
        }
        /// <summary>
        /// Sets the time method.
        /// </summary>
        public void setTimeMethod() {
            this.parse(x => x.TimeMethod, data, "Estimated Time/Probe Method");
        }
        /// <summary>
        /// Sets the time plan.
        /// </summary>
        public void setTimePlan() {
            this.parse(x => x.TimePlan, data, "Estimated Time");
        }
        /// <summary>
        /// Sets the time R2.
        /// </summary>
        public void setTimeR2() {
            this.parse(x => x.TimeR2, data, "Estimated Time/R Squared");
        }
        /// <summary>
        /// Sets the time B0.
        /// </summary>
        public void setTimeB0() {
            this.parse(x => x.TimeB0, data, "Estimated Time/Beta0");
        }
        /// <summary>
        /// Sets the time B1.
        /// </summary>
        public void setTimeB1() {
            this.parse(x => x.TimeB1, data, "Estimated Time/Beta1");
        }
        /// <summary>
        /// Sets the time range.
        /// </summary>
        public void setTimeRange() {
            this.parse(x => x.TimeRange, data, "Estimated Time/Range");
        }
        /// <summary>
        /// Sets the time UPI.
        /// </summary>
        public void setTimeUPI() {
            this.parse(x => x.TimeUPI, data, "Estimated Time/UPI");
        }
        /// <summary>
        /// Sets the time LPI.
        /// </summary>
        public void setTimeLPI() {
            this.parse(x => x.TimeLPI, data, "Estimated Time/LPI");
        }
        #endregion
    }
}
