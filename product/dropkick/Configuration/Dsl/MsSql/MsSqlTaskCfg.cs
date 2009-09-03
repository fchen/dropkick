namespace dropkick.Configuration.Dsl.MsSql
{
    public class MsSqlTaskCfg :
        DatabaseOptions,
        SqlOptions
    {
        readonly ServerOptions _options;
        string _tarantinoScripts;
        string _lightspeedBackupLocation;
        string _serverName;
        string _databaseName;

        public MsSqlTaskCfg(ServerOptions options)
        {
            _options = options;
            _serverName = options.Name;
        }

        public DatabaseOptions Database(string databaseName)
        {
            _databaseName = databaseName;
            return this;
        }

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
            _options.Part.AddTask(new OutputSqlTask(_serverName, _databaseName)
                                  {
                                      OutputSql = sql
                                  });
        }

        public void RunScript(string scriptFile)
        {
            _options.Part.AddTask(new RunSqlScriptTask(_serverName, _databaseName)
                                  {
                                      ScriptToRun = scriptFile
                                  });
        }
    }
}