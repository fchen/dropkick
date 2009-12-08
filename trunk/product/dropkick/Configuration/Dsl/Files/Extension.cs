namespace dropkick.Configuration.Dsl.Files
{
    public static class Extension
    {
        public static CopyOptions CopyTo(this Role role, string targetPath)
        {
            return new CopyTaskBuilder(role);
        }
    }
}