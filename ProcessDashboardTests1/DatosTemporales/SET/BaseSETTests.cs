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
    public class BaseSETTests {
        BaseSET set = new BaseSET();
        const int index = 1;
        DatAccessor.Datos datos = new DatAccessor.Datos() {
            { "Base_Parts/1/Actual Added", "5.0" },
            { "Base_Parts/1/Actual Base", "17.0" },
            { "Base_Parts/1/Actual Deleted", "2.0" },
            { "Base_Parts/1/Actual Modified", "2.0" },
            { "Base_Parts/1/Added", "3.0" },
            { "Base_Parts/1/Base", "17.0" },
            { "Base_Parts/1/Deleted", "5.0" },
            { "Base_Parts/1/Description", "Nodo" },
            { "Base_Parts/1/Modified", "3.0" }
        };

        public BaseSETTests() {
            set.initialize(datos, index);
            set.populate();
        }

        [TestMethod()]
        public void initializeTest() {
            Assert.AreEqual(SETType.Base, set.Type);
        }

        [TestMethod()]
        public void setNombreTest() {
            Assert.AreEqual("Nodo", set.Nombre);
        }

        [TestMethod()]
        public void setEstBaseTest() {
            Assert.AreEqual(17, set.EstBase);
        }

        [TestMethod()]
        public void setEstDeletedTest() {
            Assert.AreEqual(5, set.EstDeleted);
        }

        [TestMethod()]
        public void setEstModifiedTest() {
            Assert.AreEqual(3, set.EstModified);
        }

        [TestMethod()]
        public void setEstAddedTest() {
            Assert.AreEqual(3, set.EstAdded);
        }

        [TestMethod()]
        public void setActBaseTest() {
            Assert.AreEqual(17, set.ActBase);
        }

        [TestMethod()]
        public void setActDeletedTest() {
            Assert.AreEqual(2, set.ActDeleted);
        }

        [TestMethod()]
        public void setActModifiedTest() {
            Assert.AreEqual(2, set.ActModified);
        }

        [TestMethod()]
        public void setActAddedTest() {
            Assert.AreEqual(5, set.ActAdded);
        }

        [TestMethod()]
        public void setEstSizeTest() {
            Assert.AreEqual(15, set.EstSize);
        }

        [TestMethod()]
        public void setActSizeTest() {
            Assert.AreEqual(20, set.ActSize);
        }

        [TestMethod()]
        public void passTest() {
            Assert.IsTrue(set.pass());
        }
        
    }
    */
}