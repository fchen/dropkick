namespace dropkick.Configuration.Dsl.WinService
{
    public static class Extension
    {
        public static WinServiceOptions WinService(this Server server, string serviceName)
        {
            return new WinServiceTaskBuilder(server, serviceName);
        }
    }
}