namespace dropkick.Dsl.Dsn
{
    //http://www.codeproject.com/KB/database/DSNAdmin.aspx
    public static class Extension
    {
        public static DsnOptions CreateDSN(this ServerOptions serverOptions, string dsnName, string databaseName)
        {
            return new DsnTaskCfg(serverOptions, dsnName, databaseName);
        }
    }
}