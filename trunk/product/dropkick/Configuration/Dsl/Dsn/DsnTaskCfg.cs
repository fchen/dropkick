namespace dropkick.Configuration.Dsl.Dsn
{
    using Tasks.Dsn;

    public class DsnTaskCfg :
        DsnOptions
    {
        readonly Server _options;
        readonly DsnTask _task;

        public DsnTaskCfg(Server options, string dsnName, string databaseName)
        {
            _options = options;
            _task = new DsnTask(options.Name, dsnName, DsnAction.AddSystemDsn, DsnDriver.Sql(), databaseName);
        }
    }
}