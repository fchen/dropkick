namespace dropkick.Configuration.Dsl.CommandLine
{
    using dropkick.Dsl;
    using dropkick.Dsl.CommandLine;

    public static class Extension
    {
        public static CommandLineOptions CommandLine(this ServerOptions options, string command)
        {
            return new CommandLineTaskBuilder(options.Part, command);
        }
    }
}