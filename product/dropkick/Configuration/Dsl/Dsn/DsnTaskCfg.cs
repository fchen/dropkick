namespace dropkick.Configuration.Dsl.Dsn
{
    using Tasks.Dsn;

    public class DsnTaskCfg :
        DsnOptions
    {
        readonly DsnTask _task;

        public DsnTaskCfg(Server server, string dsnName, string databaseName)
        {
            _task = new DsnTask(server.Name, dsnName, DsnAction.AddSystemDsn, DsnDriver.Sql(), databaseName);
        }
    }
}