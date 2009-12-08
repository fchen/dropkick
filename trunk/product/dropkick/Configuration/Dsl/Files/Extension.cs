namespace dropkick.Configuration.Dsl.Files
{
    public static class Extension
    {
        public static CopyOptions CopyTo(this Server server, string targetPath)
        {
            return new CopyTaskBuilder(server);
        }
    }
}