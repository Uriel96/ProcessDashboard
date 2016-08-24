using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.Tests {
    [TestClass()]
    public class BitacoraTiempoTests {
        /*Data datos = new Data() {
            { "id", "1" },
            { "path", "/Non Project/PSP Training/Program 1/Planning" },
            { "start", "1390437969066" },
            { "delta", "4" },
            { "interrupt", "6" },
            { "comment", "Int. Termina clase, continuacion en casa" }
        };
        TimeLog bt = new TimeLog();
        */

        public BitacoraTiempoTests() {
            //bt.initialize(datos);
            //bt.populate();
        }

        [TestMethod()]
        public void setProgramaTest() {
            //Assert.AreEqual("Program 1", bt.Program);
        }

        [TestMethod()]
        public void setFaseTest() {
            //Assert.AreEqual("Planning", bt.Fase);
        }

        [TestMethod()]
        public void setInterrupcionTest() {
            //Assert.AreEqual(6, bt.Interrupt);
        }

        [TestMethod()]
        public void setDeltaTest() {
            //Assert.AreEqual(4, bt.Delta);
        }

        [TestMethod()]
        public void setComentarioTest() {
            //Assert.AreEqual("Int. Termina clase, continuacion en casa", bt.Comment);
        }

        [TestMethod()]
        public void setInicioTest() {
            //Assert.AreEqual(new DateTime(), bt.Inicio);
            Assert.Fail();
        }

        [TestMethod()]
        public void setFinTest() {
            //Assert.AreEqual(new DateTime(), bt.Fin);
            Assert.Fail();
        }
    }
}