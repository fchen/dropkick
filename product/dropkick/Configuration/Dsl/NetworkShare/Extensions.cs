namespace dropkick.Configuration.Dsl.NetworkShare
{
    public static class Extensions
    {
        public static FolderShareOptions ShareFolder(this Server server, string name)
        {
            return new FolderShareTaskCfg(server, name);
        }
    }
}