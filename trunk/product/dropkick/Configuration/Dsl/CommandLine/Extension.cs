namespace dropkick.Configuration.Dsl.CommandLine
{
    public static class Extension
    {
        public static CommandLineOptions CommandLine(this Server options, string command)
        {
            if (options.IsLocal)
                return new LocalCommandLineTaskBuilder(options.Role, command);
            else
                return new RemoteCommandLineTaskBuilder(options.Role, command);
        }
    }
}