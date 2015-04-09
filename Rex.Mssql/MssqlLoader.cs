using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rex.Lib;
using System.Data;
using System.Data.SqlClient;

namespace Rex.Mssql
{
    /// <summary>
    /// Microsoft SQL Server version of the ILoader interface.
    /// </summary>
    public class MssqlLoader : ILoader
    {
        //
        //ILoader interface
        //
        public string Name { get; private set; }

        public Database Load()
        {
            return InternalLoad();
        }

        public event EventHandler QueryExecuted;
        //
        //End ILoader interface
        //

        private void OnQueryExecuted(EventArgs sargs)
        {
            if (QueryExecuted != null)
            {
                QueryExecuted(this, sargs); //Ok to send 'this' reference as sender?
            }
        }

        private static string[] dateDataTypes = new string[] { "date", "datetime", "smalldatetime" };
        private static string[] intDataTypes = new string[] { "byte", "tinyint", "smallint", "int", "bigint" };

        private string ConnectionString { get; set; }

        private string DatabaseName { get; set; }

        public MssqlLoader(string connectionString, string loaderName, string databaseName)
        {
            ConnectionString = connectionString;
            Name = loaderName;
            DatabaseName = databaseName;
        }

        private Database InternalLoad()
        {
            string sql = @"select sch.schema_id, sch.name as schema_name, tv.object_id as tableview_id, tv.name as tableview_name,
        tv.type as tableview_type, col.column_id, col.name as column_name, typ.name as type_name, col.max_length as type_length,
		col.is_nullable, convert(bit,(case when pk.column_id is not null then 1 else 0 end)) as pk
from sys.schemas sch
inner join sys.objects tv on sch.schema_id=tv.schema_id
inner join sys.columns col on tv.object_id=col.object_id
inner join sys.types typ on col.user_type_id=typ.user_type_id
left join (select o.object_id, ic.column_id
            from sys.objects o inner join sys.indexes i on o.object_id=i.object_id and i.is_primary_key=1
            inner join sys.index_columns ic on o.object_id=ic.object_id and i.index_id=ic.index_id
          ) pk on tv.object_id=pk.object_id and col.column_id=pk.column_id
where tv.type in ('U','V')
order by sch.name, tv.name, col.name, typ.name
"; //All user tables/views with column data in all schemas in the database
            DataTable dt = AdHocSql(sql);

            Database db = new Database(DatabaseName, string.Empty); //MSSQL databases don't really have IDs
            Schema schema = null; //Schema being built from metadata -- schemas may span rows
            TableView tv = null; //Table/view being built from metadata -- tables/views may span rows
            string prevSchemaName = string.Empty; //Keeps track of schema of previous row
            string prevTVName = string.Empty;  //Keeps track of table/view of previous row

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i]; //Row contains schema, table/view, and column data
                
                //Determine database objects described by this row of metadata
                string schemaName = row["schema_name"].ToString();
                string schemaId = row["schema_id"].ToString();
                string tvName = row["tableview_name"].ToString();
                string tvId = row["tableview_id"].ToString();
                string tvTypeString = row["tableview_type"].ToString().ToLower().Trim();
                TableView.TableViewTypes tvType = (TableView.TableViewTypes)(-1); //Set initially to invalid value
                if (tvTypeString == "u")
                    tvType = TableView.TableViewTypes.Table;
                else if (tvTypeString == "v")
                    tvType = TableView.TableViewTypes.View;

                if (!schemaName.Equals(prevSchemaName))
                {
                    //Start new schema if the current row isn't a continuation of the same schema referenced by previous row
                    schema = new Schema(schemaName, schemaId, db);
                }
                if (!tvName.Equals(prevTVName))
                {
                    //Start new table/view if the current row isn't a continuation of the same table/view referenced by previous row
                    tv = new TableView(tvName, tvId, schema, tvType);
                }

                //Get column metadata for current row
                string columnName = row["column_name"].ToString();
                string columnId = row["column_id"].ToString();
                string typeName = row["type_name"].ToString();
                string typeLength = row["type_length"].ToString();
                bool isDate = false;
                bool nullable = (row["is_nullable"] == DBNull.Value) ? false : bool.Parse(row["is_nullable"].ToString());
                bool pk = (row["pk"] == DBNull.Value) ? false : bool.Parse(row["pk"].ToString());

                if (typeLength == "-1")
                {
                    //Length -1 means unlimited (max) length
                    typeName = typeName + "(max)";
                }
                else if (dateDataTypes.Contains(typeName))
                {
                    isDate = true;
                }
                else if (intDataTypes.Contains(typeName))
                {
                    //Do nothing
                }
                else
                {
                    //For other random types, add in the column length in parens
                    typeName = typeName + "(" + typeLength + ")";
                }

                Column column = new Column(columnName, columnId, tv, typeName, nullable, pk);

                //Save this row's schema and table names so we can compare them with the next row of data
                prevSchemaName = schemaName;
                prevTVName = tvName;
            }


            return db;
        }

        /// <summary>
        /// Run ad hoc SQL using a data adapter
        /// </summary>
        /// <param name="sql">Ad hoc SQL statement</param>
        /// <param name="timeoutSeconds">Optional timeout in seconds to wait for results before timing out</param>
        /// <returns>Datatable containing results</returns>
        protected DataTable AdHocSql(string sql, int timeoutSeconds = 60)
        {
            DataTable ret = new DataTable();

            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString))
            {
                adapter.SelectCommand.CommandType = CommandType.Text;
                adapter.SelectCommand.CommandTimeout = timeoutSeconds;
                adapter.Fill(ret);
            }

            return ret;
        }
    }
}
