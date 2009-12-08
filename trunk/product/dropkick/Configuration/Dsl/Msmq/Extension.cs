namespace dropkick.Configuration.Dsl.Msmq
{
    using DeploymentModel;

    public static class Extension
    {
        public static MsmqOptions Msmq(this DeploymentServer server)
        {
            return new MsmqTaskCfg(server);
        }
    }
}