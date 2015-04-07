using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;

namespace Rex.Lib.Test
{
    [TestClass]
    public class Database_Tests
    {
        [TestMethod]
        public void Constructor2a()
        {
            Database d = new Database("DB Name", "DB Id");
            Assert.AreEqual("DB Name", d.Name);
            Assert.AreEqual("DB Id", d.Id);
            Assert.IsNotNull(d.Schemas);
            Assert.AreEqual(0, d.Schemas.Count);
        }
    }
}
