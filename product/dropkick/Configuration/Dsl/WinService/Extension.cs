namespace dropkick.Configuration.Dsl.WinService
{
    public static class Extension
    {
        public static WinServiceOptions WinService(this Role role, string serviceName)
        {
            return new WinServiceTaskBuilder(role, serviceName);
        }
    }
}