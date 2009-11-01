namespace dropkick.Configuration.Dsl.CommandLine
{
    public static class Extension
    {
        public static CommandLineOptions CommandLine(this ServerOptions options, string command)
        {
            if (options.IsLocal)
                return new CommandLineTaskBuilder(options.Part, command);
            else
                return new RemoteCommandLineTaskBuilder(options.Part, command);
        }
    }
}