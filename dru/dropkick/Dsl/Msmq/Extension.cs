namespace dropkick.Dsl.Msmq
{
    public static class Extension
    {
        public static MsmqOptions Msmq(this ServerOptions serverName)
        {
            return new MsmqTaskCfg(serverName);
        }
    }
}