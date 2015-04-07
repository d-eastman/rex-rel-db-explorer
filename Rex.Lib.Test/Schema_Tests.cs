using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;

namespace Rex.Lib.Test
{
    [TestClass]
    public class Schema_Tests
    {
        [TestMethod]
        public void Constructor3a()
        {
            Database d = new Database("DB NAME", "DB ID");
            Schema s = new Schema("SCHEMA NAME", "SCHEMA ID", d);
            Assert.AreEqual("SCHEMA NAME", s.Name);
            Assert.AreEqual("SCHEMA ID", s.Id);
            Assert.AreSame(d, s.Database);
            Assert.IsNotNull(s.TablesViews);
            Assert.AreEqual(0, s.TablesViews.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor3a_NullDatabase()
        {
            Schema s = new Schema("SCHEMA NAME", "SCHEMA ID", null);
        }
    }
}
