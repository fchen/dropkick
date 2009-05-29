namespace dropkick.Dsl.MsSql
{
    using System;

    public class MsSqlTaskCfg :
        DatabaseOptions,
        SqlOptions
    {
        readonly ServerOptions _options;
        string _tarantinoScripts;
        string _lightspeedBackupLocation;
        MsSqlTask _task;

        public MsSqlTaskCfg(ServerOptions options)
        {
            _options = options;
            _task = new MsSqlTask {ServerName = _options.Name};
            _options.Part.AddTask(_task);
        }

        public DatabaseOptions Database(string databaseName)
        {
            _task.DatabaseName = databaseName;
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
            _task.OutputSql = sql;
        }
    }
}