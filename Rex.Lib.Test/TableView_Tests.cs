using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;
using System.Linq;

namespace Rex.Lib.Test
{
    [TestClass]
    public class TableView_Tests
    {
        [TestMethod]
        public void Constructor4a()
        {
            Database d = new Database("DB NAME", "DB ID");
            Schema s = new Schema("SCHEMA NAME", "SCHEMA ID", d);
            TableView tv = new TableView("TV NAME", "TV ID", s, TableView.TableViewTypes.Table);
            Assert.AreEqual("TV NAME", tv.Name);
            Assert.AreEqual("TV ID", tv.Id);
            Assert.AreSame(s, tv.Schema);
            Assert.AreEqual(TableView.TableViewTypes.Table, tv.Type);
            Assert.IsNotNull(tv.Columns);
            Assert.AreEqual(0, tv.Columns.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor4a_NullSchema()
        {
            TableView tv = new TableView("TV NAME", "TV ID", null, TableView.TableViewTypes.Table);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor4a_InvalidType()
        {
            Database d = new Database("DB NAME", "DB ID");
            Schema s = new Schema("SCHEMA NAME", "SCHEMA ID", d);
            int maxEnumValue = Enum.GetValues(typeof(TableView.TableViewTypes)).Cast<int>().Max();
            TableView.TableViewTypes invalidEnumValue = (TableView.TableViewTypes)(maxEnumValue + 1);
            TableView tv = new TableView("TV NAME", "TV ID", s, invalidEnumValue);
        }

    }
}
