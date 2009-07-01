namespace dropkick.Dsl.Dsn
{
    public class DsnTaskCfg :
        DsnOptions
    {
        private ServerOptions _options;
        private DsnTask _task;

        public DsnTaskCfg(ServerOptions options, string dsnName)
        {
            _options = options;
            _task = new DsnTask(options.Name, dsnName, DsnAction.AddSystemDsn, new DsnDriver());
        }
    }
}