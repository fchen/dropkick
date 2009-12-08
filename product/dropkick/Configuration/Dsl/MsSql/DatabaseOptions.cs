namespace dropkick.Configuration.Dsl.MsSql
{
    public interface DatabaseOptions
    {
        void OutputSql(string sql);
        void RunScript(string scriptFile);
    }
}