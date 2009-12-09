namespace dropkick.Configuration.Dsl.Dsn
{
    using DeploymentModel;
    using Tasks.Dsn;

    public class DsnTaskCfg :
        DsnOptions
    {
        readonly DsnTask _task;

        public DsnTaskCfg(DeploymentServer server, string dsnName, string databaseName)
        {
            _task = new DsnTask(server.Name, dsnName, DsnAction.AddSystemDsn, DsnDriver.Sql(), databaseName);
            server.AddDetail(_task.ToDetail(server));
        }
    }
}