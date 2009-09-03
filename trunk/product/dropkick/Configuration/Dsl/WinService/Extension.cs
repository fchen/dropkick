namespace dropkick.Configuration.Dsl.WinService
{
    public static class Extension
    {
        public static WinServiceOptions WinService(this ServerOptions server, string serviceName)
        {
            return new WinServiceTaskBuilder(server, serviceName);
        }
    }
}