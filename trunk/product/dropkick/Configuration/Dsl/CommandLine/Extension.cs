namespace dropkick.Configuration.Dsl.CommandLine
{
    using DeploymentModel;

    public static class Extension
    {
        public static CommandLineOptions CommandLine(this Server server, string command)
        {
            //this needs to be deferred
            if (true) //(server.IsLocal)
                return new LocalCommandLineTaskBuilder(server, command);
            else
                return new RemoteCommandLineTaskBuilder(server, command);
        }
    }
}