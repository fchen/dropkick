namespace dropkick.Configuration.Dsl.NetworkShare
{
    using dropkick.Dsl;
    using dropkick.Dsl.NetworkShare;

    public static class Extensions
    {
        public static FolderShareOptions ShareFolder(this ServerOptions server, string name)
        {
            return new FolderShareTaskCfg(server, name);
        }
    }
}