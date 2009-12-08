namespace dropkick.Configuration.Dsl.WinService
{
    using DeploymentModel;

    public static class Extension
    {
        public static WinServiceOptions WinService(this DeploymentServer server, string serviceName)
        {
            return new WinServiceTaskBuilder(server, serviceName);
        }
    }
}