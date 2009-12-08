namespace dropkick.Configuration.Dsl.NetworkShare
{
    using DeploymentModel;

    public static class Extensions
    {
        public static FolderShareOptions ShareFolder(this DeploymentServer server, string name)
        {
            return new FolderShareTaskCfg(server, name);
        }
    }
}