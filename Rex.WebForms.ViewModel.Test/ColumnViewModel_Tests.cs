using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;
using Rex.WebForms.ViewModel;
using System.Collections.Generic;

namespace Rex.WebForms.ViewModel.Test
{
    [TestClass]
    public class ColumnViewModel_Tests
    {
        private ColumnViewModel GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader()
        {
            return new ColumnViewModel(new DatabaseWithOneSchemaOneTableTwoColumnTestLoader(),
                "CONN ID", "DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID",
                "DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 ID");
        }

        private ColumnViewModel GenerateNewViewModel_DatabaseWithOneSchemaOneTableTestLoader()
        {
            return new ColumnViewModel(new DatabaseWithOneSchemaOneTableTestLoader(),
                "CONN ID", "DatabaseWithOneSchemaOneTableTestLoader SCHEMA ID",
                "DatabaseWithOneSchemaOneTableTestLoader TV ID", "");
        }

        private ColumnViewModel GenerateNewViewModel_DatabaseWithNoSchemasTestLoader()
        {
            return new ColumnViewModel(new DatabaseWithNoSchemasTestLoader(), "CONN ID", "", "", "");
        }

        [TestMethod]
        public void ConnId()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("CONN ID", v.ConnId);
        }

        [TestMethod]
        public void SchemaId()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader SCHEMA ID", v.SchemaId);
        }

        [TestMethod]
        public void TVId()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV ID", v.TVId);
        }

        [TestMethod]
        public void ColumnId()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 ID", v.ColumnId);
        }

        [TestMethod]
        public void TVName()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader TV NAME", v.TVName);
        }

        [TestMethod]
        public void TVTypeName()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("Table", v.TVTypeName);
        }

        [TestMethod]
        public void ColumnName()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("DatabaseWithOneSchemaOneTableTwoColumnTestLoader COLUMN1 NAME", v.ColumnName);
        }

        [TestMethod]
        public void ColumnTypeName()
        {
            ColumnViewModel v = GenerateNewViewModel_DatabaseWithOneSchemaOneTableTwoColumnTestLoader();
            Assert.AreEqual("int", v.ColumnTypeName);
        }

    }
}
