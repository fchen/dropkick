namespace dropkick.Configuration.Dsl.MsSql
{
    using DeploymentModel;

    public static class Extension
    {
        public static SqlOptions SqlInstance(this DeploymentServer server, string instanceName)
        {
            return new MsSqlTaskCfg(server);
        }
    }
}