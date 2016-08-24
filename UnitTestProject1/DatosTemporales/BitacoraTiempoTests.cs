using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDashboard.DatosTemporales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.DatosTemporales.Tests {
    [TestClass()]
    public class BitacoraTiempoTests {

        LectorXML.Atributos datos = new LectorXML.Atributos() {
            { "id", "1" },
            { "path", "/Non Project/PSP Training/Program 1/Planning" },
            { "start", "1390437969066" },
            { "delta", "4" },
            { "interrupt", "6" },
            { "comment", "Int. Termina clase, continuacion en casa" }
        };
        BitacoraTiempo bt = new BitacoraTiempo();

        public BitacoraTiempoTests() {
            bt.initialize(datos);
        }

        [TestMethod()]
        public void initializeTest() {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void setProgramaTest() {
            bt.setPrograma();
            Assert.AreEqual("Program 1", bt.Programa);
        }

        [TestMethod()]
        public void setFaseTest() {
            bt.setFase();
            Assert.AreEqual("Planning", bt.Fase);
        }

        [TestMethod()]
        public void setInterrupcionTest() {
            bt.setInterrupcion();
            Assert.AreEqual(6, bt.Interrupcion);
        }

        [TestMethod()]
        public void setDeltaTest() {
            bt.setDelta();
            Assert.AreEqual(4, bt.Delta);
        }

        [TestMethod()]
        public void setComentarioTest() {
            bt.setComentario();
            Assert.AreEqual("Int. Termina clase, continuacion en casa", bt.Comentario);
        }

        [TestMethod()]
        public void setInicioTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void setFinTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void obtenerBTTest() {
            bt.obtenerBT();
            Assert.AreEqual("Int. Termina clase, continuacion en casa", bt.Comentario);
            Assert.AreEqual(4, bt.Delta);
            Assert.AreEqual("Planning", bt.Fase);
            //Assert.AreEqual(new DateTime(), bt.Inicio);
            //Assert.AreEqual(new DateTime(), bt.Fin);
            Assert.AreEqual("Program 1", bt.Programa);
        }
    }
}