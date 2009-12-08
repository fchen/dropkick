namespace dropkick.Configuration.Dsl.MsSql
{
    using Tasks.MsSql;

    public class MsSqlTaskCfg :
        DatabaseOptions,
        SqlOptions
    {
        readonly Server _options;
        readonly string _serverName;
        string _databaseName;
        string _lightspeedBackupLocation;
        string _tarantinoScripts;

        public MsSqlTaskCfg(Server options)
        {
            _options = options;
            _serverName = options.Name;
        }

        #region DatabaseOptions Members

        public DatabaseOptions RunTarantinoOn(string locationOfScripts)
        {
            _tarantinoScripts = locationOfScripts;
            return this;
        }

        public DatabaseOptions BackupWithLightspeedTo(string backupLocation)
        {
            _lightspeedBackupLocation = backupLocation;
            return this;
        }

        public void OutputSql(string sql)
        {
            _options.Role.AddTask(new OutputSqlTask(_serverName, _databaseName)
                                  {
                                      OutputSql = sql
                                  });
        }

        public void RunScript(string scriptFile)
        {
            _options.Role.AddTask(new RunSqlScriptTask(_serverName, _databaseName)
                                  {
                                      ScriptToRun = scriptFile
                                  });
        }

        #endregion

        #region SqlOptions Members

        public DatabaseOptions Database(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }

        #endregion
    }
}