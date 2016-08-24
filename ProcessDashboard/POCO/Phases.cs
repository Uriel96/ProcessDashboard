using ProcessDashboard.Accessors;
using static ProcessDashboard.HelperClasses.DataUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ProcessDashboard.POCO.POCO_SET;
using ProcessDashboard.HelperClasses;
using ProcessDashboard.GenerationProcess;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds all the Phases Information of a Program.
    /// </summary>
    public class Phases : List<Phase> {
        #region Properties        
        /// <summary>
        /// The Planning Phase.
        /// </summary>
        public Phase Planning { get; private set; } = new Phase();
        /// <summary>
        /// The Design Phase.
        /// </summary>
        public Phase Design { get; private set; } = new Phase();
        /// <summary>
        /// The Review Phase.
        /// </summary>
        public Phase DesignReview { get; private set; } = new Phase();
        /// <summary>
        /// The Code Phase.
        /// </summary>
        public Phase Code { get; private set; } = new Phase();
        /// <summary>
        /// The Code Review Phase.
        /// </summary>
        public Phase CodeReview { get; private set; } = new Phase();
        /// <summary>
        /// The Compile Phase.
        /// </summary>
        public Phase Compile { get; private set; } = new Phase();
        /// <summary>
        /// The Testing Phase.
        /// </summary>
        public Phase Test { get; private set; } = new Phase();
        /// <summary>
        /// The Postmortem Phase.
        /// </summary>
        public Phase Postmortem { get; private set; } = new Phase();

        /// <summary>
        /// The Total Minutes To Date.
        /// </summary>
        public float TotalToDateMin { get; private set; }
        /// <summary>
        /// The Total Defects Injected To Date.
        /// </summary>
        public float TotalToDateInj { get; private set; }
        /// <summary>
        /// The Total Defects Removed To Date.
        /// </summary>
        public float TotalToDateRem { get; private set; }
        /// <summary>
        /// The Total Estimated Minutes.
        /// </summary>
        public float TotalEstMin { get; private set; }
        /// <summary>
        /// The Total Estimated Defects Injected.
        /// </summary>
        public float TotalEstInj { get; private set; }
        /// <summary>
        /// The Total Lines of Code To Date.
        /// </summary>
        public float TotalToDateLOC { get; private set; }


        private Data data;
        private SETS sets;
        private PROBE probe;
        private Phases previousPhases;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="sets">The sets already generated of the program.</param>
        /// <param name="probe">The probe of the program.</param>
        /// <param name="previousPhases">The phases of the previous program.</param>
        public Phases(Data data, SETS sets, PROBE probe, Phases previousPhases) {
            this.data = data;
            this.sets = sets;
            this.probe = probe;
            this.previousPhases = previousPhases;
            //Sets Phases
            AddRange(new Phase[] { Planning, Design, DesignReview, Code, CodeReview, Compile, Test, Postmortem });
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        /// <param name="initializationAction">The action that initializes phases base properties.</param>
        public void populate(Action<Phase> initializationAction) {
            setTotalEstMin();
            setTotalToDateLOC();
            setTotalEstInj();
            //Iterates through each phase and populates it with data
            for (int index = 0; index < Count; index++) {
                if (!Enum.IsDefined(typeof(PhaseType), index))
                    continue;
                var phase = this[index];
                phase.initialize(data, this, previousPhases, (PhaseType)index);
                initializationAction.Invoke(phase);
                phase.populate();
                //Adds To Date Data to the Total
                addTotalToDateMin(phase);
                addTotalToDateInj(phase);
                addTotalToDateRem(phase);
            }

            if (Test.Program >= Program.Program3) {
                setTestEstMin();
            }
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Adds to the Total To Date Minutes.
        /// </summary>
        /// <param name="phase">The phase to be added Information.</param>
        public void addTotalToDateMin(Phase phase) {
            TotalToDateMin += phase.ToDateMin;
        }
        /// <summary>
        /// Adds to the Total To Date Defects Injected.
        /// </summary>
        /// <param name="phase">The phase to be added Information.</param>
        public void addTotalToDateInj(Phase phase) {
            TotalToDateInj += phase.ToDateInj;
        }
        /// <summary>
        /// Adds to the Total To Date Defects Removed.
        /// </summary>
        /// <param name="phase">The phase to be added Information.</param>
        public void addTotalToDateRem(Phase phase) {
            TotalToDateRem += phase.ToDateRem;
        }

        /// <summary>
        /// Sets the Total Estimated Minutes.
        /// </summary>
        public void setTotalEstMin() {
            TotalEstMin = data.getOrDefualt("Estimated Time").parse<float>();
        }
        /// <summary>
        /// Sets the Total Estimated Defects Injected.
        /// </summary>
        public void setTotalEstInj() {
            TotalEstInj = (previousPhases == null) ? 0 : div(previousPhases.TotalToDateInj * probe.SizePlan, previousPhases.TotalToDateLOC);
        }
        /// <summary>
        /// Sets the Total To Date Lines of Code.
        /// </summary>
        public void setTotalToDateLOC() {
            TotalToDateLOC = sets.TotalActAM + (previousPhases?.TotalToDateLOC ?? 0);
        }
        /// <summary>
        /// Sets the Test Phase to the Total Estimated Time Minus All other Estimated Time Phases.
        /// </summary>
        public void setTestEstMin() {
            Test.EstMin = TotalEstMin;
            foreach (var phase in this) {
                if (phase != Test) {
                    Test.EstMin -= phase.EstMin;
                }
            }
        }
        #endregion
    }
}
