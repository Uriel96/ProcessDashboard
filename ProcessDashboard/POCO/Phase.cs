using ProcessDashboard.Accessors;
using static ProcessDashboard.HelperClasses.DataUtil;
using ProcessDashboard.GenerationProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ProcessDashboard.HelperClasses;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// The Phases when doing a PSP Program.
    /// </summary>
    public enum PhaseType {
        /// <summary>
        /// Phase where all the Planning of the Software is made.
        /// </summary>
        Planning,
        /// <summary>
        /// Phase where Software is Designed.
        /// </summary>
        Design,
        /// <summary>
        /// Phase where the Design is Reviewed.
        /// </summary>
        DesignReview,
        /// <summary>
        /// Phase where all the Code is made.
        /// </summary>
        Code,
        /// <summary>
        /// Phase where the Code Generated must be Reviewed.
        /// </summary>
        CodeReview,
        /// <summary>
        /// Phase where the Code is Compile to see the Product.
        /// </summary>
        Compile,
        /// <summary>
        /// Phase where the Code is Tested to ensure everything is working as expected.
        /// </summary>
        Test,
        /// <summary>
        /// Final Phase where everything made is analysed and Documentation is made.
        /// </summary>
        Postmortem
    };

    /// <summary>
    /// Class that holds the Information of a Phase.
    /// </summary>
    public class Phase : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the Phase Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /// <summary>
        /// The Type of Phase (Plan, Design, Code, etc).
        /// </summary>
        public PhaseType Type { get; set; }
        /// <summary>
        /// The Estimated Minutes.
        /// </summary>
        public float EstMin { get; set; }
        /// <summary>
        /// The Estimated Defects Injected.
        /// </summary>
        public float EstDefInj { get; set; }
        /// <summary>
        /// The Estimated Defects Removed.
        /// </summary>
        public float EstDefRem { get; set; }
        /// <summary>
        /// The Estimated Yield.
        /// </summary>
        public float EstYield { get; set; }

        /// <summary>
        /// The Actual Minutes.
        /// </summary>
        public float ActMin { get; set; }
        /// <summary>
        /// The Actual Defects Injected.
        /// </summary>
        public float ActDefInj { get; set; }
        /// <summary>
        /// The Actual Defects Removed.
        /// </summary>
        public float ActDefRem { get; set; }
        /// <summary>
        /// The Actual Yield.
        /// </summary>
        public float ActYield { get; set; }

        /// <summary>
        /// The Minutes To Date of all the previous Phases.
        /// </summary>
        [NotMapped]
        public float ToDateMin { get; set; }
        /// <summary>
        /// The Defects Injected To Date of all the previous Phases.
        /// </summary>
        [NotMapped]
        public float ToDateInj { get; set; }
        /// <summary>
        /// The Defects Removed To Date of all the previous Phases.
        /// </summary>
        [NotMapped]
        public float ToDateRem { get; set; }
        /// <summary>
        /// The Present Estimated Defects Injected Until this Phase.
        /// </summary>
        [NotMapped]
        public float EstPresent { get; set; }
        /// <summary>
        /// The Present Actual Defects Injected Until this Phase.
        /// </summary>
        [NotMapped]
        public float ActPresent { get; set; }
        #endregion

        #region Fields        
        /// <summary>
        /// The data that will be taken.
        /// </summary>
        [NotMapped]
        private Data data;
        /// <summary>
        /// The phases of the program.
        /// </summary>
        [NotMapped]
        private Phases phases;
        /// <summary>
        /// The phases of the previous program.
        /// </summary>
        [NotMapped]
        private Phases previousPhases;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="phases">The phases of the program.</param>
        /// <param name="previousPhases">The phases of the previous program.</param>
        /// <param name="Type">The type of Phase (Plan, Design, etc).</param>
        public void initialize(Data data, Phases phases, Phases previousPhases, PhaseType Type) {
            this.data = data;
            this.phases = phases;
            this.previousPhases = previousPhases;
            this.Type = Type;
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            setEstMin();
            if (previousPhases != null && Program >= Program.Program3) {
                setEstDefIny();
                setEstDefRem();
            }
            setEstYield();

            setActMin();
            setActDefInj();
            setActDefRem();
            setActYield();

            setToDateMin();
            setToDateInj();
            setToDateRem();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the Estimated Minutes.
        /// </summary>
        public void setEstMin() {
            //Checks if Estimated Time exists in the Data, if not, it Calculates it
            if (!this.parse(x => x.EstMin, data, $"{Type.getPhase()}/Estimated Time")) {
                EstMin = div(phases.TotalEstMin * (getCurrent(previousPhases)?.ToDateMin ?? 0), previousPhases?.TotalToDateMin ?? 0);
            }
        }
        /// <summary>
        /// Sets the Estimated Defects Injected.
        /// </summary>
        public void setEstDefIny() {
            //Checks if Estimated Defects injected exists in the Data, if not, it Calculates it
            if (!this.parse(x => x.EstDefInj, data, $"{Type.getPhase()}/Estimated Defects Injected")) {
                EstDefInj = phases.TotalEstInj * (getCurrent(previousPhases)?.ToDateInj ?? 0) / previousPhases.TotalToDateInj;
            }
        }
        /// <summary>
        /// Sets the Estimated Defects Removed.
        /// </summary>
        public void setEstDefRem() {
            //Checks if Estimated Defects Removed exists in the Data, if not, it Calculates it
            if (!this.parse(x => x.EstDefRem, data, $"{Type.getPhase()}/Estimated Defects Removed")) {
                EstDefRem = phases.TotalEstInj * (getCurrent(previousPhases)?.ToDateRem ?? 0) / previousPhases.TotalToDateRem;
            }
        }
        /// <summary>
        /// Sets the Estimated Yield.
        /// </summary>
        public void setEstYield() {
            EstPresent = EstDefInj + (getPrevious(phases)?.EstPresent ?? 0) - (getPrevious(phases)?.EstDefRem ?? 0);
            EstYield = div(EstDefRem * 100, EstPresent, 100, 1);
        }

        /// <summary>
        /// Sets the Actual Minutes.
        /// </summary>
        public void setActMin() {
            this.parse(x => x.ActMin, data, $"{Type.getPhase()}/Time");
        }
        /// <summary>
        /// Sets the Actual Defects Injected
        /// </summary>
        public void setActDefInj() {
            this.parse(x => x.ActDefInj, data, $"{Type.getPhase()}/Defects Injected");
        }
        /// <summary>
        /// Sets the Actual Defects Removed.
        /// </summary>
        public void setActDefRem() {
            this.parse(x => x.ActDefRem, data, $"{Type.getPhase()}/Defects Removed");
        }
        /// <summary>
        /// Sets the Actual Yield.
        /// </summary>
        public void setActYield() {
            ActPresent = ActDefInj + (getPrevious(phases)?.ActPresent ?? 0) - (getPrevious(phases)?.ActDefRem ?? 0);
            ActYield = div(ActDefRem * 100, ActPresent, 100);
        }

        /// <summary>
        /// Sets the to Date Actual Minutes.
        /// </summary>
        public void setToDateMin() {
            ToDateMin = ActMin + (getCurrent(previousPhases)?.ToDateMin ?? 0);
        }
        /// <summary>
        /// Sets the to Date Actual Defects Injected.
        /// </summary>
        public void setToDateInj() {
            ToDateInj = ActDefInj + (getCurrent(previousPhases)?.ToDateInj ?? 0);
        }
        /// <summary>
        /// Sets the to Date Actual Defects Removed.
        /// </summary>
        public void setToDateRem() {
            ToDateRem = ActDefRem + (getCurrent(previousPhases)?.ToDateRem ?? 0);
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Checks if the necesary Information is in the Data.
        /// </summary>
        /// <returns>Returns whether or not the necesary Information is in the Data.</returns>
        /// <remarks>Might be later deprecated.</remarks>
        public bool pass() {
            return data.exists(
                $"{Type.getPhase()}/Time",
                $"{Type.getPhase()}/Defects Injected",
                $"{Type.getPhase()}/Defects Removed");
        }

        /// <summary>
        /// Gets the previous phase of the phases of a program.
        /// </summary>
        /// <param name="phases">The phases of a program.</param>
        /// <returns>Returns the previous phase.</returns>
        public Phase getPrevious(Phases phases) {
            if ((int)Type - 1 >= 0)
                return phases?[(int)Type - 1];
            else
                return null;
        }
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <param name="phases">The phases.</param>
        /// <returns></returns>
        public Phase getCurrent(Phases phases) {
            return phases?[(int)Type];
        }
        #endregion
    }
}
