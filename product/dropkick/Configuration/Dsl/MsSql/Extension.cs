namespace dropkick.Configuration.Dsl.MsSql
{
    public static class Extension
    {
        public static SqlOptions SqlInstance(this ServerOptions server, string instanceName)
        {
            return new MsSqlTaskCfg(server);
        }
    }
}