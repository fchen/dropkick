namespace dropkick.Configuration.Dsl.Msmq
{
    public static class Extension
    {
        public static MsmqOptions Msmq(this Role role)
        {
            return new MsmqTaskCfg(role);
        }
    }
}