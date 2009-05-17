namespace dropkick.Dsl.MsSql
{
    public interface DatabaseOptions
    {
        DatabaseOptions Verify();
        DatabaseOptions RunTarantinoOn(string locationOfScripts);
        DatabaseOptions BackupWithLightspeedTo(string backupLocation);
    }
}