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
    public class FaseTests {
        /*Phase faseAnterior = new Phase() {
            Program = "Program 1",
            EstDefInj = 10,
            EstMin = 15,
            EstDefRem = 20,
            EstPresent = 10,
            EstYield = 5,
            ActDefInj = 10,
            ActDefRem = 20,
            ActMin = 14,
            ActPresent = 20,
            ActYield = 23,
            ToDateInj = 10,
            ToDateMin = 14
        };
        DatAccessor.Datos datos = new DatAccessor.Datos() {
            { "Compile/Completed", "@1391036975204" },
            { "Compile/Defects Injected", "1.0" },
            { "Compile/Defects Removed", "9.0" },
            { "Compile/Started", "@1391035660113" },
            { "Compile/Time", "13.0" }
        };
        Phase fase = new Phase();
        */

        public FaseTests() {
            //fase.initialize(datos, PhaseType.Compile);
        }

        [TestMethod()]
        public void initializeTest() {
            //Assert.AreEqual(PhaseType.Compile, fase.Type);
        }

        [TestMethod()]
        public void getTipoTest() {
            //Assert.AreEqual("Compile", fase.getTipe());
        }

        [TestMethod()]
        public void setEstMinTest() {
            //fase.setEstMin(,);
            //Assert.AreEqual(, fase.EstMin);
            Assert.Fail();
        }

        [TestMethod()]
        public void setEstDefInyTest() {
            //fase.setEstDefIny(,);
            //Assert.AreEqual(, fase.EstDefIny);
            Assert.Fail();
        }

        [TestMethod()]
        public void setEstDefRemTest() {
            //fase.setEstDefRem();
            //Assert.AreEqual(, fase.EstDefRem);
            Assert.Fail();
        }

        [TestMethod()]
        public void setEstYieldTest() {
            //fase.setEstYield(faseAnterior);
            //Assert.AreEqual(, fase.EstYield);
            Assert.Fail();
        }

        [TestMethod()]
        public void setResMinTest() {
            //fase.setActMin();
            //Assert.AreEqual(13, fase.ActMin);
        }

        [TestMethod()]
        public void setResDefInyTest() {
            //fase.setActDefInj();
            //Assert.AreEqual(1, fase.ActDefInj);
        }

        [TestMethod()]
        public void setResDefRemTest() {
            //fase.setActDefRem();
            //Assert.AreEqual(9, fase.ActDefRem);
        }

        [TestMethod()]
        public void setResYieldTest() {
            //fase.setResYield(faseAnterior);
            //Assert.AreEqual(, fase.ResYield);
            Assert.Fail();
        }

        [TestMethod()]
        public void passTest() {
            //Assert.IsTrue(fase.pass());
        }

        [TestMethod()]
        public void setMinAFechaTest() {
            //fase.setActMin();
            //fase.setMinAFecha(faseAnterior);
            //Assert.AreEqual(27, fase.ToDateMin);
        }

        [TestMethod()]
        public void setInyectadasAFechaTest() {
            //fase.setActDefInj();
            //fase.setInyectadasAFecha(faseAnterior);
            //Assert.AreEqual(11, fase.ToDateInj);
        }

        [TestMethod()]
        public void obtenerFaseTest() {
            Assert.Fail();
        }

    }
}