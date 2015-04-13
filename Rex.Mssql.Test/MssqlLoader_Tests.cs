using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rex.Lib;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace Rex.Mssql.Test
{
    [TestClass]
    public class MssqlLoader_Tests
    {
        //SQLCMD utility downloaded from http://www.microsoft.com/en-us/download/details.aspx?id=36433
        private static readonly string SQLCMD_EXE = @"C:\Program Files\Microsoft SQL Server\110\Tools\Binn\SQLCMD.EXE";
        private static readonly string TEST_SERVER = @".\MSSQL2012";
        private static readonly string TEST_DATABASE = "RexUnitTesting";
        private static readonly string TEST_CONN_STRING = @"Server=.\MSSQL2012;Database=RexUnitTesting;Integrated Security=SSPI";
        private static readonly string DROP_ALL_TABLES_VIEWS_SCRIPT = @"C:\temp\Rex\Rex.Mssql.Test\T-SqlScripts\drop all user tables views.sql";
        private static readonly string CREATE_Table4Columns0Rows_TABLE_SCRIPT = @"C:\temp\Rex\Rex.Mssql.Test\T-SqlScripts\drop create Table4Columns0Rows table.sql";
        private static readonly string CREATE_ViewOfTable4Columns0Rows_VIEW_SCRIPT = @"C:\temp\Rex\Rex.Mssql.Test\T-SqlScripts\drop create ViewOfTable4Columns0Rows view.sql";

        [TestMethod]
        public void Constructor2a()
        {
            MssqlLoader m = new MssqlLoader("CONNECTION STRING", "LOADER NAME", "DB NAME");
            //ConnectionString is not exposed, so cannot check it
            Assert.AreEqual("LOADER NAME", m.Name);
        }

        [TestMethod]
        public void Load_Table4Columns0Rows_ViewOfTable4Columns0Rows()
        {
            //Preparation
            Assert.AreEqual(0, RunSQLCMD(DROP_ALL_TABLES_VIEWS_SCRIPT)); 
            Assert.AreEqual(0, RunSQLCMD(CREATE_Table4Columns0Rows_TABLE_SCRIPT)); 
            Assert.AreEqual(0, RunSQLCMD(CREATE_ViewOfTable4Columns0Rows_VIEW_SCRIPT)); 

            //Load what was prepared
            MssqlLoader m = new MssqlLoader(TEST_CONN_STRING, "ConnTest", "DbTest");
            Database d = m.Load();

            Assert.IsNotNull(d);
            Assert.IsNotNull(d.Schemas);
            Assert.AreEqual(1, d.Schemas.Count);

            Schema s = d.Schemas[0];
            Assert.IsNotNull(s);
            Assert.AreSame(d, s.Database);
            Assert.AreEqual("dbo", s.Name);
            Assert.IsNotNull(s.TablesViews);
            Assert.AreEqual(2, s.TablesViews.Count);
            
            TableView t = s.TablesViews.Where(x => x.Type == TableView.TableViewTypes.Table).SingleOrDefault();
            Assert.IsNotNull(t);
            Assert.AreSame(s, t.Schema);
            Assert.AreEqual("Table4Columns0Rows", t.Name);
            Assert.IsNotNull(t.Columns);
            Assert.AreEqual(4, t.Columns.Count);

            Column c = t.Columns[0];
            Assert.AreEqual("Column1", c.Name);
            Assert.AreEqual("int", c.TypeName);
            Assert.AreEqual(false, c.Nullable);
            Assert.AreEqual(true, c.PrimaryKey);
            Assert.AreSame(t, c.TableView);

            c = t.Columns[1];
            Assert.AreEqual("Column2", c.Name);
            Assert.AreEqual("date", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(t, c.TableView);

            c = t.Columns[2];
            Assert.AreEqual("Column3", c.Name);
            Assert.AreEqual("varchar(max)", c.TypeName);
            Assert.AreEqual(false, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(t, c.TableView);

            c = t.Columns[3];
            Assert.AreEqual("Column4", c.Name);
            Assert.AreEqual("varbinary(50)", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(t, c.TableView);

            //To prevent copy-paste errors in next section that SHOULD reference v not t
            c = null;
            t = null; 

            TableView v = s.TablesViews.Where(x => x.Type == Lib.TableView.TableViewTypes.View).SingleOrDefault();
            Assert.IsNotNull(v);
            Assert.AreSame(s, v.Schema);
            Assert.AreEqual("ViewOfTable4Columns0Rows", v.Name);
            Assert.IsNotNull(v.Columns);
            Assert.AreEqual(5, v.Columns.Count);

            c = v.Columns[0];
            Assert.AreEqual("Column1", c.Name);
            Assert.AreEqual("int", c.TypeName);
            Assert.AreEqual(false, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey); //View unable to tell this is part of table's PK
            Assert.AreSame(v, c.TableView);

            c = v.Columns[1];
            Assert.AreEqual("Column2", c.Name);
            Assert.AreEqual("date", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey); 
            Assert.AreSame(v, c.TableView);

            c = v.Columns[2];
            Assert.AreEqual("Column3", c.Name);
            Assert.AreEqual("varchar(max)", c.TypeName);
            Assert.AreEqual(false, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(v, c.TableView);

            c = v.Columns[3];
            Assert.AreEqual("Column4", c.Name);
            Assert.AreEqual("varbinary(50)", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(v, c.TableView);

            c = v.Columns[4];
            Assert.AreEqual("TenTimesColumn1", c.Name);
            Assert.AreEqual("int", c.TypeName);
            Assert.AreEqual(true, c.Nullable);
            Assert.AreEqual(false, c.PrimaryKey);
            Assert.AreSame(v, c.TableView);
        }

        [TestMethod]
        public void QueryExecuted()
        {
            bool eventOccurred = false; 
            MssqlLoader m = new MssqlLoader(TEST_CONN_STRING, "ConnTest", "DbTest");
            m.QueryExecuted += (object sender, QueryExecutedEventArgs e) =>
            {
                eventOccurred = true;
                Assert.IsNotNull(sender);
                Assert.IsNotNull(e);
            };
            Database d = m.Load();
            Assert.IsTrue(eventOccurred, "QueryExecuted should have occurred");
        }

        [TestMethod]
        public void QueryExecuted_NoSubscribers()
        {
            bool eventOccurred = false;
            MssqlLoader m = new MssqlLoader(TEST_CONN_STRING, "ConnTest", "DbTest");
            //Do not subscribe to the m.QueryOccurred event
            Database d = m.Load(); //Triggers QueryExecuted event
            Assert.IsFalse(eventOccurred, "QueryExecuted should not have occurred");
        }

        /// <summary>
        /// Run a T-SQL script file using the SQLCMD.EXE command line utility in Integrated Security mode on the
        /// test server and test database.
        /// </summary>
        /// <param name="scriptFilename">Filename of script to run</param>
        /// <returns>SQLCMD.EXE exit code which will be zero unless there was an error</returns>
        private int RunSQLCMD(string scriptFilename)
        {
            //-E argument means use integrated security (connect to SQL Server as current user)
            ProcessStartInfo psi = new ProcessStartInfo(SQLCMD_EXE, String.Format("-E -S{0} -d{1} -i\"{2}\"", TEST_SERVER, TEST_DATABASE, scriptFilename));
            psi.WindowStyle = ProcessWindowStyle.Hidden; //No DOS window flashes
            Process p = Process.Start(psi);
            p.WaitForExit();
            return p.ExitCode;
        }
    }
}
