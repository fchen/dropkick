namespace dropkick.Configuration.Dsl.Msmq
{
    public static class Extension
    {
        public static MsmqOptions Msmq(this Server serverName)
        {
            return new MsmqTaskCfg(serverName);
        }
    }
}