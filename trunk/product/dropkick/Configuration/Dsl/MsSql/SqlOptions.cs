namespace dropkick.Configuration.Dsl.MsSql
{
    public interface SqlOptions
    {
        DatabaseOptions Database(string databaseName);
    }
}