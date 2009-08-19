namespace dropkick.Dsl.MsSql
{
    public interface DatabaseOptions
    {
        DatabaseOptions RunTarantinoOn(string locationOfScripts);
        DatabaseOptions BackupWithLightspeedTo(string backupLocation);
        void OutputSql(string sql);
        void RunScript(string scriptFile);
    }
}