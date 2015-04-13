using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;
using System;
using System.Collections.Generic;

namespace Rex.WebForms.ViewModel.Test
{
    [TestClass]
    public class DatabaseViewModel_Tests
    {
        private DatabaseViewModel GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader()
        {
            return new DatabaseViewModel(new DatabaseWithOneSchemaOneTableTestLoader(), "CONN ID");
        }

        private DatabaseViewModel GenerateNewViewModel_DatabaseWithNoSchemasTestLoader()
        {
            return new DatabaseViewModel(new DatabaseWithNoSchemasTestLoader(), "CONN ID");
        }

        [TestMethod]
        public void ConnId()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithNoSchemasTestLoader();
            Assert.AreEqual("CONN ID", v.ConnId);
        }

        [TestMethod]
        public void SchemaId()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader();
            Assert.IsTrue(String.IsNullOrEmpty(v.SchemaId));
        }

        [TestMethod]
        public void TVId()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader();
            Assert.IsTrue(String.IsNullOrEmpty(v.TVId));
        }

        [TestMethod]
        public void ColumnId()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader();
            Assert.IsTrue(String.IsNullOrEmpty(v.ColumnId));
        }

        [TestMethod]
        public void TVDisplayData()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader();
            List<TVDisplayData> data = v.TVDisplayData;
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Count);
            TVDisplayData tv = data[0];
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTestLoader SCHEMA NAME", tv.SchemaName);
            Assert.AreEqual("TV.aspx?conn=CONN ID&sch=DatabaseWithOneSchemaOneTableTestLoader " +
                "SCHEMA ID&tv=DatabaseWithOneSchemaOneTableTestLoader TV ID", tv.TVLink);
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTestLoader TV NAME", tv.TVName);
            Assert.AreEqual("Table", tv.TVTypeName);
        }

        [TestMethod]
        public void TVDisplayData_DatabaseWithNoSchemasTestLoader()
        {
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithNoSchemasTestLoader();
            List<TVDisplayData> t = v.TVDisplayData;
            Assert.IsNotNull(t);
            Assert.AreEqual(0, t.Count);
        }

        [TestMethod]
        public void QueryExecuted()
        {
            bool eventOccurred = false;
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithNoSchemasTestLoader();
            v.QueryExecuted += (object sender, QueryExecutedEventArgs e) =>
            {
                eventOccurred = true;
                Assert.IsNotNull(sender);
                Assert.IsNotNull(e);
            };
            List<TVDisplayData> t = v.TVDisplayData; //Triggers QueryExecuted event
            Assert.IsTrue(eventOccurred, "QueryExecuted event failed to occur");
        }

        [TestMethod]
        public void QueryExecuted_NoSubscribers()
        {
            bool eventOccurred = false;
            DatabaseViewModel v = GenerateNewViewModel_DatabaseWithNoSchemasTestLoader();
            //Do not subscribe to the v.QueryExecuted event
            List<TVDisplayData> t = v.TVDisplayData; //Triggers QueryExecuted event
            Assert.IsFalse(eventOccurred, "QueryExecuted event should not have occurred");
        }

    }
}