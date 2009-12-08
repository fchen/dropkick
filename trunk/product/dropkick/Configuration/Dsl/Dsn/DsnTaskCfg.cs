namespace dropkick.Configuration.Dsl.Dsn
{
    using Tasks.Dsn;

    public class DsnTaskCfg :
        DsnOptions
    {
        readonly Role _role;
        readonly DsnTask _task;

        public DsnTaskCfg(Role role, string dsnName, string databaseName)
        {
            _role = role;
            //TODO: fix - not role.name
            _task = new DsnTask(role.Name, dsnName, DsnAction.AddSystemDsn, DsnDriver.Sql(), databaseName);
        }
    }
}