namespace dropkick.Configuration.Dsl.NetworkShare
{
    public static class Extensions
    {
        public static FolderShareOptions ShareFolder(this Role role, string name)
        {
            return new FolderShareTaskCfg(role, name);
        }
    }
}