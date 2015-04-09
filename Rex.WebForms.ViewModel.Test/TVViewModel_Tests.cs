using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Rex.WebForms.ViewModel.Test
{
    [TestClass]
    public class TVViewModel_Tests
    {
        private TVViewModel GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader()
        {
            return new TVViewModel(new DatabaseWithOneSchemaOneTableTwoColumnTestLoader(),
                "CONN ID", "DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID");
        }

        private TVViewModel GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader()
        {
            return new TVViewModel(new DatabaseWithOneSchemaOneTableTestLoader(),
                "CONN ID", "DatabaseWithOneSchemaOneTableTestLoader SCHEMA ID",
                "DatabaseWithOneSchemaOneTableTestLoader TV ID");
        }

        private TVViewModel GenerateNewViewModel_DatabaseWithNoSchemasTestLoader()
        {
            return new TVViewModel(new DatabaseWithNoSchemasTestLoader(), "CONN ID", "", "");
        }

        [TestMethod]
        public void ConnId()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("CONN ID", v.ConnId);
        }

        [TestMethod]
        public void SchemaId()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID", v.SchemaId);
        }

        [TestMethod]
        public void TVId()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID", v.TVId);
        }

        [TestMethod]
        public void ColumnId()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.IsTrue(String.IsNullOrEmpty(v.ColumnId));
        }

        [TestMethod]
        public void TVName()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV NAME", v.TVName);
        }

        [TestMethod]
        public void TVTypeName()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("Table", v.TVTypeName);
        }

        [TestMethod]
        public void ColumnDisplayData()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            List<ColumnDisplayData> data = v.ColumnDisplayData;
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Count);
            ColumnDisplayData c1 = data[0];
            Assert.AreEqual("Column.aspx?conn=CONN ID&sch=DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID" +
                "&tv=DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID" + 
                "&col=DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 ID", c1.ColumnLink);
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 NAME", c1.ColumnName);
            Assert.AreEqual("int", c1.ColumnTypeName);
            Assert.AreEqual("Req'd", c1.Nullability);
            Assert.AreEqual("PK", c1.PK);
            c1 = null; //Prevent copy paste errors when asserting on another column

            ColumnDisplayData c2 = data[1];
            Assert.AreEqual("Column.aspx?conn=CONN ID&sch=DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID" +
                "&tv=DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID" +
                "&col=DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN2 ID", c2.ColumnLink);
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN2 NAME", c2.ColumnName);
            Assert.AreEqual("varchar(50)", c2.ColumnTypeName);
            Assert.AreEqual("Null", c2.Nullability);
            Assert.IsTrue(String.IsNullOrEmpty(c2.PK));
        }

        [TestMethod]
        public void ColumnDisplayData_DatabaseWithOneSchemaOneTableTestLoader()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader();
            List<ColumnDisplayData> data = v.ColumnDisplayData;
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ColumnDisplayData_DatabaseWithNoSchemasTestLoader()
        {
            TVViewModel v = GenerateNewViewModel_DatabaseWithNoSchemasTestLoader();
            List<ColumnDisplayData> data = v.ColumnDisplayData; //No schemas, so this blows up
        }
    }
}