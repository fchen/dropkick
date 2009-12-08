namespace dropkick.Configuration.Dsl.CommandLine
{
    using DeploymentModel;

    public static class Extension
    {
        public static CommandLineOptions CommandLine(this DeploymentServer server, string command)
        {
            if (server.IsLocal)
                return new LocalCommandLineTaskBuilder(server, command);
            else
                return new RemoteCommandLineTaskBuilder(server, command);
        }
    }
}