namespace dropkick.Configuration.Dsl.Iis
{
    public static class Extension
    {
        public static IisSiteOptions IisSite(this Server server, string websiteName)
        {
            return IisVersionSelector.SelectTheCorrectConfig(server, websiteName);
        }
        public static IisSiteOptions Iis6Site(this Server server, string websiteName)
        {
            return new Iis6TaskCfg(server, websiteName);
        }

        public static IisSiteOptions Iis7Site(this Server server, string websiteName)
        {
            return new Iis7TaskCfg(server, websiteName);
        }
    }
}