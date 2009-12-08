namespace dropkick.Configuration.Dsl.Dsn
{
    public static class Extension
    {
        public static DsnOptions CreateDSN(this Role role, string dsnName, string databaseName)
        {
            return new DsnTaskCfg(role, dsnName, databaseName);
        }
    }
}