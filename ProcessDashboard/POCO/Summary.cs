using ProcessDashboard.Accessors;
using static ProcessDashboard.HelperClasses.DataUtil;
using System;
using ProcessDashboard;
using System.Linq;
using ProcessDashboard.GenerationProcess;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProcessDashboard.POCO.POCO_SET;
using ProcessDashboard.HelperClasses;

namespace ProcessDashboard.POCO {
    /// <summary>
    /// Class that holds a Summary Information of the Program.
    /// </summary>
    public class Summary : DashboardTable {
        #region Properties        
        /// <summary>
        /// A unique Identifier for the Summary Information.
        /// </summary>
        [Key]
        public override int ID { get; set; }
        /// <summary>
        /// The Estimated Lines Of Code.
        /// </summary>
        public float EstLOC { get; set; }
        /// <summary>
        /// The Estimated Minutes.
        /// </summary>
        public float EstMin { get; set; }
        /// <summary>
        /// The Estimated Defects Injected.
        /// </summary>
        public float EstDef { get; set; }
        /// <summary>
        /// The Estimated Productivity.
        /// </summary>
        public float EstProd { get; set; }
        /// <summary>
        /// The Estimated Yield.
        /// </summary>
        public float EstYield { get; set; }

        /// <summary>
        /// The Actual Size Base.
        /// </summary>
        public float ActSizeB { get; set; }
        /// <summary>
        /// The Actual Size Deleted.
        /// </summary>
        public float ActSizeD { get; set; }
        /// <summary>
        /// The Actual Size Modified.
        /// </summary>
        public float ActSizeM { get; set; }
        /// <summary>
        /// The Actual Size Added.
        /// </summary>
        public float ActSizeA { get; set; }
        /// <summary>
        /// The Actual Size Reused.
        /// </summary>
        public float ActSizeR { get; set; }
        /// <summary>
        /// The Actual Total Size.
        /// </summary>
        public float ActSizeT { get; set; }

        /// <summary>
        /// The Actual Lines of Code.
        /// </summary>
        public float ActLOC { get; set; }
        /// <summary>
        /// The Actual Minutes.
        /// </summary>
        public float ActMin { get; set; }
        /// <summary>
        /// The Actual Defects Injected.
        /// </summary>
        public float ActDef { get; set; }
        /// <summary>
        /// The Actual Productivity.
        /// </summary>
        public float ActProd { get; set; }
        /// <summary>
        /// The Actual Yield.
        /// </summary>
        public float ActYield { get; set; }

        /// <summary>
        /// The Actual PQI.
        /// </summary>
        public float ActPQI { get; set; }
        /// <summary>
        /// The Actual Percent Design Review (Detail Lavel Design Review).
        /// </summary>
        public float ActDLDR { get; set; }
        /// <summary>
        /// The Actual Percent Code Review.
        /// </summary>
        public float ActCR { get; set; }
        /// <summary>
        /// The Actual Code Review Rate (Review Velocity).
        /// </summary>
        public float ActRevVel { get; set; }
        /// <summary>
        /// The Actual CPI.
        /// </summary>
        public float ActCPI { get; set; }
        /// <summary>
        /// The Actual Defect Density (Total Defects/KLOC).
        /// </summary>
        public float ActDensDef { get; set; }
        /// <summary>
        /// The Actual Defects Compile And Test.
        /// </summary>
        public float ActDefCT { get; set; }
        /// <summary>
        /// The Actual AFR.
        /// </summary>
        public float ActAFR { get; set; }
        /// <summary>
        /// The Actual Defects Per Hour in Design Review (DLDR).
        /// </summary>
        public float ActDHDLDR { get; set; }
        /// <summary>
        /// The Actual Defects Per Hour in Code Review.
        /// </summary>
        public float ActDHCR { get; set; }
        /// <summary>
        /// The Actual Defects Per Hour in Unit Test.
        /// </summary>
        public float ActDHUT { get; set; }
        /// <summary>
        /// The Actual Defect Remove Leverage for Design Review (DLDR).
        /// </summary>
        public float ActDRLDLDR { get; set; }
        /// <summary>
        /// The Actual Defect Remove Leverage for Code Review.
        /// </summary>
        public float ActDRLCR { get; set; }

        /// <summary>
        /// The Actual Defects Injected To Date.
        /// </summary>
        [NotMapped]
        public float ToDateActDef { get; set; }
        /// <summary>
        /// The Actual Lines of Code To Date.
        /// </summary>
        [NotMapped]
        public float ToDateActLOC { get; set; }
        #endregion

        #region Fields
        [NotMapped]
        private Data data;
        [NotMapped]
        private Phases previousPhases;
        [NotMapped]
        private SETS sets;
        [NotMapped]
        private Summary previousSummary;
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes the Data needed to process the Information.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="previousPhases">The phases of the previous program.</param>
        /// <param name="sets">The sets already generated of the program.</param>
        /// <param name="previousSummary">The summary of the previous program.</param>
        public void initialize(Data data, Phases previousPhases, SETS sets, Summary previousSummary) {
            this.data = data;
            this.previousPhases = previousPhases;
            this.sets = sets;
            this.previousSummary = previousSummary;

            ToDateActDef = previousSummary?.ToDateActDef ?? 0;
            ToDateActLOC = previousSummary?.ToDateActLOC ?? 0;
        }
        #endregion

        #region Main Methods        
        /// <summary>
        /// Processes and Sets all the Information needed (populates the Information).
        /// </summary>
        public override void populate() {
            setEstLOC();
            setEstMin();
            if (Program >= Program.Program3) {
                setEstDef();
            }
            setEstProd();
            if (Program >= Program.Program3) {
                setEstYield();
            }
            setActSizeB();
            setActSizeD();
            setActSizeM();
            setActSizeR();
            setActSizeT();
            setActSizeA();
            setActLOC();
            setActMin();
            setActDef();
            setActProd();
            setActYield();
            setActPQI();
            setActDLDR();
            setActCR();
            setActRevVel();
            setActCPI();
            setActDensDef();
            setActDefCT();
            setActAFR();
            setActDHDLDR();
            setActDHCR();
            setActDHUT();
            setActDRLDLDR();
            setActDRLCR();
        }
        #endregion

        #region Setters        
        /// <summary>
        /// Sets the Estimated Lines of Code.
        /// </summary>
        public void setEstLOC() {
            this.parse(x => x.EstLOC, data, $"Estimated New & Changed LOC");
        }
        /// <summary>
        /// Sets the Estimated Minutes.
        /// </summary>
        public void setEstMin() {
            this.parse(x => x.EstMin, data, $"Estimated Time");
        }
        /// <summary>
        /// Sets the Estimated Defects Injected.
        /// </summary>
        public void setEstDef() {
            EstDef = previousPhases.TotalEstInj;
        }
        /// <summary>
        /// Sets the Estimated Productivity.
        /// </summary>
        public void setEstProd() {
            EstProd = div(EstLOC * 60, EstMin);
        }
        /// <summary>
        /// Sets the Estimated Yield.
        /// </summary>
        public void setEstYield() {
            var totalDefIny = previousPhases.Sum(phase => phase.Type < PhaseType.Compile ? phase.EstDefInj : 0);
            var totalDefRem = previousPhases.Sum(phase => phase.Type < PhaseType.Compile ? phase.EstDefRem : 0);
            if (totalDefRem != 0 && totalDefIny == 0)
                EstYield = -99;
            else if (totalDefRem == 0 && totalDefIny == 0)
                EstYield = 100;
            else
                EstYield = totalDefRem * 100 / totalDefIny;
        }

        /// <summary>
        /// Sets the Actual Base Size.
        /// </summary>
        public void setActSizeB() {
            ActSizeB = sets.TotalActBase;
        }
        /// <summary>
        /// Sets the Actual Deleted Size.
        /// </summary>
        public void setActSizeD() {
            ActSizeD = sets.TotalActDeleted;
        }
        /// <summary>
        /// Sets the Actual Modified Size.
        /// </summary>
        public void setActSizeM() {
            ActSizeM = sets.TotalActModified;
        }
        /// <summary>
        /// Sets the Actual Reused Size.
        /// </summary>
        public void setActSizeR() {
            ActSizeR = sets.TotalActReused;
        }
        /// <summary>
        /// Sets the Actual Total Size.
        /// </summary>
        public void setActSizeT() {
            ActSizeT = sets.TotalAct;
        }
        /// <summary>
        /// Sets the Actual Added Size.
        /// </summary>
        public void setActSizeA() {
            ActSizeA = sets.TotalActAdded;
        }

        /// <summary>
        /// Sets the Actual Lines of Code.
        /// </summary>
        public void setActLOC() {
            ActLOC = sets.TotalActAM;
            ToDateActLOC = ActLOC;
        }
        /// <summary>
        /// Sets the Actual Minutes.
        /// </summary>
        public void setActMin() {
            ActMin = previousPhases.Sum(x => x.ActMin);
        }
        /// <summary>
        /// Sets the Actual Defects Injected.
        /// </summary>
        public void setActDef() {
            ActDef = previousPhases.Sum(x => x.ActDefInj);
            ToDateActDef = ActDef;
        }
        /// <summary>
        /// Sets the Actual Productivity.
        /// </summary>
        public void setActProd() {
            ActProd = div(ActLOC * 60, ActMin);
        }
        /// <summary>
        /// Sets the Actual Yield.
        /// </summary>
        public void setActYield() {
            var totalDefIny = previousPhases.Sum(phase => phase.Type < PhaseType.Compile ? phase.ActDefInj : 0);
            var totalDefRem = previousPhases.Sum(phase => phase.Type < PhaseType.Compile ? phase.ActDefRem : 0);

            if (totalDefRem != 0 && totalDefIny == 0)
                ActYield = -99;
            else if (totalDefRem == 0 && totalDefIny == 0)
                ActYield = 100;
            else
                ActYield = totalDefRem * 100 / totalDefIny;
        }

        /// <summary>
        /// Sets the Actual PQI.
        /// </summary>
        public void setActPQI() {
            var a = div(previousPhases.Design.ActMin, previousPhases.Code.ActMin, -1);
            var b = div(previousPhases.DesignReview.ActMin * 2, previousPhases.Design.ActMin, -1);
            var c = div(previousPhases.CodeReview.ActMin * 2, previousPhases.Code.ActMin, -1);
            var d = div(20, div(10 + previousPhases.Compile.ActDefRem * 1000, ActLOC, -1), -1);
            var e = div(10, div(5 + previousPhases.Test.ActDefRem * 1000, ActLOC, -1), -1);
            if (a < 0 || b < 0 || c < 0 || d < 0 || e < 0) {
                ActPQI = -1;
            } else {
                if (a > 1)
                    a = 1;
                if (b > 1)
                    b = 1;
                if (c > 1)
                    c = 1;
                if (d > 1)
                    d = 1;
                if (e > 1)
                    e = 1;
                ActPQI = a * b * c * d * e;
            }
        }
        /// <summary>
        /// Sets the Actual DLDR.
        /// </summary>
        public void setActDLDR() {
            ActDLDR = div(previousPhases.DesignReview.ActMin * 100, previousPhases.Design.ActMin);
        }
        /// <summary>
        /// Sets the Actual CR.
        /// </summary>
        public void setActCR() {
            ActCR = div(previousPhases.CodeReview.ActMin * 100, previousPhases.Code.ActMin);
        }
        /// <summary>
        /// Sets the Actual Review Velocity.
        /// </summary>
        public void setActRevVel() {
            ActRevVel = div(ActLOC * 60, previousPhases.CodeReview.ActMin);
        }
        /// <summary>
        /// Sets the Actual CPI.
        /// </summary>
        public void setActCPI() {
            ActCPI = div(EstMin, ActMin);
        }
        /// <summary>
        /// Sets the Actual Defects Density.
        /// </summary>
        public void setActDensDef() {
            ActDensDef = div(ActDef * 1000, ActLOC);
        }
        /// <summary>
        /// Sets the Actual Defects in Code and Testing.
        /// </summary>
        public void setActDefCT() {
            ActDefCT = div((previousPhases.Compile.ActDefRem + previousPhases.Test.ActDefRem) * 100, ActDef);
        }
        /// <summary>
        /// Sets the Actual AFR.
        /// </summary>
        public void setActAFR() {
            ActAFR = div(previousPhases.DesignReview.ActMin + previousPhases.CodeReview.ActMin,
                previousPhases.Compile.ActMin + previousPhases.Test.ActMin);
        }
        /// <summary>
        /// Sets the Actual DHDLDR.
        /// </summary>
        public void setActDHDLDR() {
            ActDHDLDR = div(previousPhases.DesignReview.ActDefRem * 60, previousPhases.DesignReview.ActMin);
        }
        /// <summary>
        /// Sets the Actual DHCR.
        /// </summary>
        public void setActDHCR() {
            ActDHCR = div(previousPhases.CodeReview.ActDefRem * 60, previousPhases.CodeReview.ActMin);
        }
        /// <summary>
        /// Sets the Actual DHUT.
        /// </summary>
        public void setActDHUT() {
            ActDHUT = div(previousPhases.Test.ActDefRem * 60, previousPhases.Test.ActMin);
        }
        /// <summary>
        /// Sets the Actual DRLDLDR.
        /// </summary>
        public void setActDRLDLDR() {
            ActDRLDLDR = div(ActDHDLDR, ActDHUT, 999);
        }
        /// <summary>
        /// Sets the Actual DRLCR.
        /// </summary>
        public void setActDRLCR() {
            ActDRLCR = div(ActDHCR, ActDHUT, 999);
        }
        #endregion
    }
}
