namespace dropkick.Dsl.MsSql
{
    public static class Extension
    {
        public static SqlOptions SqlInstance(this ServerOptions server, string instanceName)
        {
            return null;
        }
    }

    public interface SqlOptions
    {
        DatabaseOptions Database(string databaseName);
    }

    public interface DatabaseOptions
    {
        DatabaseOptions Verify();
        DatabaseOptions RunTarantinoOn(string locationOfScripts);
        DatabaseOptions BackupWithLightspeedTo(string backupLocation);
    }
}