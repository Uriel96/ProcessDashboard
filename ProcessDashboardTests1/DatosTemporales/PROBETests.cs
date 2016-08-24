using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDashboard.POCO;
using ProcessDashboard.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.Tests {
    [TestClass()]
    public class PROBETests {
        /*PROBE probe = new PROBE();
        DatAccessor.Datos datos = new DatAccessor.Datos() {
            { "Estimated New & Changed LOC/Beta0", "2.0" },
            { "Estimated New & Changed LOC/Beta1", "1.1116279069767443" },
            { "Estimated New & Changed LOC/Interval Percent", "N/A" },
            { "Estimated New & Changed LOC/LPI", "3.4" },
            { "Estimated New & Changed LOC/Probe Method", "C" },
            { "Estimated New & Changed LOC/R Squared", "N/A" },
            { "Estimated New & Changed LOC/Range", "5" },
            { "Estimated New & Changed LOC/UPI", "N/A" },
            { "Estimated New & Changed LOC", "102.93674418604651" },
            { "Estimated Time/Beta0", "6.0" },
            { "Estimated Time/Beta1", "6.181395348837209" },
            { "Estimated Time/Interval Percent", "N/A" },
            { "Estimated Time/LPI", "20" },
            { "Estimated Time/Probe Method", "C1" },
            { "Estimated Time/R Squared", "5" },
            { "Estimated Time/Range", "7" },
            { "Estimated Time/UPI", "9" },
            { "Estimated Time", "590.0" },
            { "PROBE_LIST", "/Non Project/PSP Training/Program 1/Non Project/PSP Training/Program 2" },
            { "PROBE_Last_Run_Value/Estimated Added & Modified Size", "102.93674418604651" },
            { "PROBE_Last_Run_Value/Estimated Proxy Size", "92.6" },
            { "PROBE_Last_Run_Value/Estimated Time", "572.3972093023256" }
        };*/

        public PROBETests() {
            //probe.initialize(datos);
            //probe.populate();
        }

        [TestMethod()]
        public void setTamProxyTest() {
            //Assert.AreEqual(92.6f, probe.SizeProxy);
        }

        [TestMethod()]
        public void setTamMetodoTest() {
            //Assert.AreEqual("C", probe.SizeMethod);
        }

        [TestMethod()]
        public void setTamPlanTest() {
            //Assert.AreEqual(102.93674418604651f, probe.SizePlan);
        }

        [TestMethod()]
        public void setTamR2Test() {
            //Assert.AreEqual(0, probe.SizeR2);
        }

        [TestMethod()]
        public void setTamB0Test() {
            //Assert.AreEqual(2, probe.SizeB0);
        }

        [TestMethod()]
        public void setTamB1Test() {
            //Assert.AreEqual(1.1116279069767443f, probe.SizeB1);
        }

        [TestMethod()]
        public void setTamRangoTest() {
            //Assert.AreEqual(5, probe.SizeRange);
        }

        [TestMethod()]
        public void setTamUPITest() {
            //Assert.AreEqual(0, probe.SizeUPI);
        }

        [TestMethod()]
        public void setTamLPITest() {
            //Assert.AreEqual(3.4f, probe.SizeLPI);
        }

        [TestMethod()]
        public void setTieMetodoTest() {
            //Assert.AreEqual("C1", probe.TimeMethod);
        }

        [TestMethod()]
        public void setTiePlanTest() {
            //Assert.AreEqual(590, probe.TimePlan);
        }

        [TestMethod()]
        public void setTieR2Test() {
            //Assert.AreEqual(5, probe.TimeR2);
        }

        [TestMethod()]
        public void setTieB0Test() {
            //Assert.AreEqual(6, probe.TimeB0);
        }

        [TestMethod()]
        public void setTieB1Test() {
            //Assert.AreEqual(6.181395348837209f, probe.TimeB1);
        }

        [TestMethod()]
        public void setTieRangoTest() {
            //Assert.AreEqual(7, probe.TimeRange);
        }

        [TestMethod()]
        public void setTieUPITest() {
            //Assert.AreEqual(9, probe.TimeUPI);
        }

        [TestMethod()]
        public void setTieLPITest() {
            //Assert.AreEqual(20, probe.TimeLPI);
        }
    }
}