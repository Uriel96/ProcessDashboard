using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessDashboard.POCO;
using ProcessDashboard.Accessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessDashboard.POCO.Tests {
    //[TestClass()]
    /*
    public class NewSETTests {
        NewSET set = new NewSET();
        const int index = 0;
        DatAccessor.Datos datos = new DatAccessor.Datos() {
            { "New Objects/0/Actual LOC", "52.0" },
            { "New Objects/0/Actual Methods", "2.0" },
            { "New Objects/0/Description", "Main" },
            { "New Objects/0/LOC", "65.0" },
            { "New Objects/0/Methods", "2.0" },
            { "New Objects/0/Relative Size", "Small" },
            { "New Objects/0/Type", "I/O"}
        };

        public NewSETTests() {
            set.initialize(datos, index);
            set.populate();
        }

        [TestMethod()]
        public void initializeTest() {
            Assert.AreEqual(SETType.New, set.Type);
        }

        [TestMethod()]
        public void setDescriptionTest() {
            Assert.AreEqual("Main", set.Nombre);
        }

        [TestMethod()]
        public void setEstSizeTest() {
            Assert.AreEqual(65, set.EstSize);
        }

        [TestMethod()]
        public void setEstTipoTest() {
            Assert.AreEqual("I/O", set.EstTipo);
        }

        [TestMethod()]
        public void setEstItemsTest() {
            Assert.AreEqual(2, set.EstItems);
        }

        [TestMethod()]
        public void setEstRelSizeTest() {
            Assert.AreEqual("Small", set.EstRelSize);
        }

        [TestMethod()]
        public void setEstNRTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void setActSizeTest() {
            Assert.AreEqual(52, set.ActSize);
        }

        [TestMethod()]
        public void setActItemsTest() {
            Assert.AreEqual(2, set.ActItems);
        }

        [TestMethod()]
        public void setActNRTest() {
            Assert.Fail();
        }

        [TestMethod()]
        public void passTest() {
            Assert.IsTrue(set.pass());
        }
        
    }
    */
}