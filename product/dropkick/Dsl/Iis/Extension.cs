namespace dropkick.Dsl.Iis
{
    public static class Extension
    {
        public static IisSiteOptions Iis6Site(this ServerOptions server, string websiteName)
        {
            return new Iis6TaskCfg(server, websiteName);
        }

        public static IisSiteOptions Iis7Site(this ServerOptions server, string websiteName)
        {
            return new Iis7TaskCfg(server, websiteName);
        }
    }
}