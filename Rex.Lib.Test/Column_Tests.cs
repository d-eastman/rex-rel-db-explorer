using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;

namespace Rex.Lib.Test
{
    [TestClass]
    public class Column_Tests
    {
        [TestMethod]
        public void Constructor6a()
        {
            Database d = new Database("DB NAME", "DB ID");
            Schema s = new Schema("SCHEMA NAME", "SCHEMA ID", d);
            TableView tv = new TableView("TV NAME", "TV ID", s, TableView.TableViewTypes.View);
            Column c = new Column("COL NAME", "COL ID", tv, "int", true, true);
            Assert.AreEqual("COL NAME", c.Name);
            Assert.AreEqual("COL ID", c.Id);
            Assert.AreEqual("int", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(true, c.PrimaryKey);            
            Assert.AreSame(tv, c.TableView);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Constructor6a_NullTV()
        {
            Column c = new Column("COL NAME", "COL ID", null, "int", true, true);
        }
    }
}
