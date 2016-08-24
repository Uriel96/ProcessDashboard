using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDashboard.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.Tests {
    [TestClass()]
    public class BitacoraDefectosTests {
        /*XMLAccessor.Atributos datos = new XMLAccessor.Atributos() {
            { "num", "2" },
            { "type", "Build, package" },
            { "inj", "Code" },
            { "rem", "Compile" },
            { "ft", "1.8" },
            { "fd", "3.0" },
            { "count", "2" },
            { "desc", "Programa2.cpp: falto agregar libreria &lt;vector&gt; " },
            { "date", "@1393111109032" }
        };
        BitacoraDefectos bd = new BitacoraDefectos();
        */
        public BitacoraDefectosTests() {
            //bd.initialize(datos);
            //bd.populate();
        }

        [TestMethod()]
        public void setFaseInyectadoTest() {
            //Assert.AreEqual("Code", bd.PhaseInjected);
        }

        [TestMethod()]
        public void setFaseRemovidoTest() {
            //Assert.AreEqual("Compile", bd.PhaseRemoved);
        }

        [TestMethod()]
        public void setFaseDefectoTest() {
            //Assert.AreEqual("Build, package", bd.DefectType);
        }

        [TestMethod()]
        public void setDescripcionTest() {
            //Assert.AreEqual("Programa2.cpp: falto agregar libreria &lt;vector&gt; ", bd.Description);
        }

        [TestMethod()]
        public void setFechaTest() {
            //Assert.AreEqual(new Date(), bd.Fecha);
            Assert.Fail();
        }

        [TestMethod()]
        public void setIDDefectoTest() {
            //Assert.AreEqual(2, bd.DefectID);
        }

        [TestMethod()]
        public void setFixDefectTest() {
            //Assert.AreEqual(3, bd.FixDefect);
        }

        [TestMethod()]
        public void setFixCountTest() {
            //Assert.AreEqual(2, bd.FixCount);
        }

        [TestMethod()]
        public void setFixTimeTest() {
            //Assert.AreEqual(1.8f, bd.FixTime);
        }
    }
}