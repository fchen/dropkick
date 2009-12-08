namespace dropkick.Configuration.Dsl.CommandLine
{
    public static class Extension
    {
        public static CommandLineOptions CommandLine(this Server server, string command)
        {
            if (server.IsLocal)
                return new LocalCommandLineTaskBuilder(server, command);
            else
                return new RemoteCommandLineTaskBuilder(server, command);
        }
    }
}