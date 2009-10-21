namespace dropkick.Configuration.Dsl.Dsn
{
    public static class Extension
    {
        public static DsnOptions CreateDSN(this ServerOptions serverOptions, string dsnName, string databaseName)
        {
            return new DsnTaskCfg(serverOptions, dsnName, databaseName);
        }
    }
}