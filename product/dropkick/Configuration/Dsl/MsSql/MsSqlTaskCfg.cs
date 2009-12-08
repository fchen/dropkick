namespace dropkick.Configuration.Dsl.MsSql
{
    using Tasks.MsSql;

    public class MsSqlTaskCfg :
        DatabaseOptions,
        SqlOptions
    {
        readonly Server _server;
        readonly string _serverName;
        string _databaseName;

        public MsSqlTaskCfg(Server server)
        {
            _server = server;
            _serverName = server.Name;
        }

        #region DatabaseOptions Members

        public void OutputSql(string sql)
        {
            _server.Role.AddTask(new OutputSqlTask(_serverName, _databaseName)
                                  {
                                      OutputSql = sql
                                  });
        }

        public void RunScript(string scriptFile)
        {
            _server.Role.AddTask(new RunSqlScriptTask(_serverName, _databaseName)
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