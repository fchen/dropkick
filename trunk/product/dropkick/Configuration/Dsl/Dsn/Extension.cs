namespace dropkick.Configuration.Dsl.Dsn
{
    using DeploymentModel;

    public static class Extension
    {
        public static DsnOptions CreateDSN(this DeploymentServer server, string dsnName, string databaseName)
        {
            return new DsnTaskCfg(server, dsnName, databaseName);
        }
    }
}