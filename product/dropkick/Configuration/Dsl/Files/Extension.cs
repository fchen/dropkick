namespace dropkick.Configuration.Dsl.Files
{
    public static class Extension
    {
        public static CopyOptions CopyFrom(this Role role, string sourcePath)
        {
            //because I don't use the server name I had to do this upcast
            return new CopyTaskBuilder(sourcePath, (RoleCfg)role);
        }
    }
}