namespace dropkick.Configuration.Dsl.MsSql
{
    using DeploymentModel;
    using Tasks.MsSql;

    public class MsSqlTaskCfg :
        DatabaseOptions,
        SqlOptions
    {
        readonly DeploymentServer _server;
        readonly string _serverName;
        string _databaseName;

        public MsSqlTaskCfg(DeploymentServer server)
        {
            _server = server;
            _serverName = server.Name;
        }

        #region DatabaseOptions Members

        public void OutputSql(string sql)
        {
            _server.AddDetail(new OutputSqlTask(_serverName, _databaseName)
                                  {
                                      OutputSql = sql
                                  }.ToDetail(_server));
        }

        public void RunScript(string scriptFile)
        {
            _server.AddDetail(new RunSqlScriptTask(_serverName, _databaseName)
                                  {
                                      ScriptToRun = scriptFile
                                  }.ToDetail(_server));
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