using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;

namespace Rex.Lib.Test
{
    [TestClass]
    public class QueryExecutedEventArgs_Tests
    {
        [TestMethod]
        public void Constructor1a()
        {
            QueryExecutedEventArgs e = new QueryExecutedEventArgs("select * from whatever");
            Assert.AreEqual("select * from whatever", e.Sql);
            Assert.AreEqual(typeof(EventArgs), e.GetType().BaseType);
        }
    }
}
