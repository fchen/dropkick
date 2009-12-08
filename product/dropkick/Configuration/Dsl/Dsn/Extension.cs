namespace dropkick.Configuration.Dsl.Dsn
{
    public static class Extension
    {
        public static DsnOptions CreateDSN(this Server server, string dsnName, string databaseName)
        {
            return new DsnTaskCfg(server, dsnName, databaseName);
        }
    }
}