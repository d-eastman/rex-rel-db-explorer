using Rex.Lib;
using Rex.Mssql;
using Rex.WebForms.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

/// <summary>
/// Load up configured database connections and the appropriate concrete ILoader object
/// 
/// Eventually, I would like to abstract this away and have an ILoaderFactory or something like that
/// and one of the concrete classes implementing that interface will be initialized with a
/// System.Configuration.ConnectionStringSettingsCollection object (or maybe a Dictionary<string,string> with
/// key-value pairs) passed in from app.config or web.config.  
/// Might have another implementation that reads connection string configuration from a different
/// kind of config file, like a simple text file or read in from a database table that contains config info.
/// </summary>
public class LoaderWebFactory
{
    //
    //Singleton functionality
    //

    private static object _LockObject = new object();
    private static LoaderWebFactory _SingletonInstance;

    /// <summary>
    /// Private constructor prevents external instantiation
    /// </summary>
    private LoaderWebFactory()
    {
    }

    /// <summary>
    /// Single entry point to getting an instance of the class
    /// </summary>
    /// <returns>Singleton instance of the class</returns>
    public static LoaderWebFactory GetSingletonInstance()
    {
        if (_SingletonInstance == null)
        {
            lock (_LockObject)
            {
                {
                    _SingletonInstance = new LoaderWebFactory();
                }
            }
        }
        return _SingletonInstance;
    }

    //
    //Instance functionality
    //

    /// <summary>
    /// Given connection name (that corresponds to what's found in web.config), return an appropriate ILoader concrete object
    /// to handle that database connection.
    /// </summary>
    /// <param name="connName">Name of connection that appears in web.config</param>
    /// <returns>Concrete ILoader object capable to handling the database connection</returns>
    public ILoader GetMetaLoader(string connName)
    {
        ILoader ret = null;
        if (!String.IsNullOrEmpty(connName) && ConfigurationManager.ConnectionStrings[connName] != null)
        {
            ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings[connName];
            if (cs.ProviderName.ToLower().Equals("system.data.sqlclient"))
            {
                string databaseName = cs.Name; //Database name defaults to connection string name

                //Try to determine actual database name by parsing connection string
                string[] connStringKeyValuePairs = cs.ConnectionString.Split(';');
                foreach (string pair in connStringKeyValuePairs)
                {
                    string[] splitPair = pair.Split('=');
                    string[] databaseKeyNames = new string[] { "attachdbfilename", "database", "initial catalog" };
                    if (databaseKeyNames.Contains(splitPair[0].Trim().ToLower()))
                    {
                        databaseName = splitPair[1].Trim();
                        break;
                    }
                }

                ret = new MssqlLoader(cs.ConnectionString, cs.Name, databaseName);
            }
            //TODO: add more providers
        }
        return ret;
    }

    /// <summary>
    /// Look in web.config for all database connections
    /// </summary>
    /// <returns>Strongly typed list of database connection properties</returns>
    public List<DbConnData> GetDatabaseConnections()
    {
        List<DbConnData> ret = new List<DbConnData>();

        string[] serverKeyNames = new string[] { "server", "data source" }; //In a connection string, these indicate the server name
        string[] databaseKeyNames = new string[] { "attachdbfilename", "database", "initial catalog", "dbq" }; //In a connection string, these indicate database name
        string[] userKeyNames = new string[] { "integrated security", "uid", "user", "userid", "username" }; //In a connection string, these indicate user

        List<DbConnData> conns = new List<DbConnData>();
        foreach (var conn in ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>())
        {
            if (conn.Name == "LocalSqlServer")
                continue; //Exclude this automatic connection string

            DbConnData cd = new DbConnData
            {
                Provider = conn.ProviderName.Replace("System.Data.", ""),
                Name = conn.Name
            };

            //Split connection string into an array of key-value pairs and then plug the value into the appropriate property

            foreach (string connStringKeyValuePairsJoined in conn.ConnectionString.Split(';'))
            {
                string[] connStringKeyValuePairsSplit;
                if ((connStringKeyValuePairsSplit = connStringKeyValuePairsJoined.Split('=')).Length == 2)
                {
                    if (serverKeyNames.Contains(connStringKeyValuePairsSplit[0].ToLower()) && String.IsNullOrEmpty(cd.Server))
                    {
                        cd.Server = connStringKeyValuePairsSplit[1];
                    }
                    else if (databaseKeyNames.Contains(connStringKeyValuePairsSplit[0].ToLower()) && String.IsNullOrEmpty(cd.Database))
                    {
                        cd.Database = connStringKeyValuePairsSplit[1];
                    }
                    else if (userKeyNames.Contains(connStringKeyValuePairsSplit[0].ToLower()) && String.IsNullOrEmpty(cd.ConnectAs) &&
                        !String.IsNullOrEmpty(connStringKeyValuePairsSplit[1]) && !connStringKeyValuePairsSplit[1].ToLower().Equals("false"))
                    {
                        cd.ConnectAs = connStringKeyValuePairsSplit[1];
                        if (cd.ConnectAs.ToLower() == "sspi")
                        {
                            //SSPI / Integrated Security means connecting as current user
                            cd.ConnectAs += " (" + Environment.UserName + ")";
                        }
                    }
                }
            }
            ret.Add(cd);
        }

        return ret;
    }
}