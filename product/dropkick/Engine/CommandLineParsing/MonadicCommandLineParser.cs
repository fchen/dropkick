namespace dropkick.Engine.CommandLineParsing
{
    using System.Collections.Generic;
    using Magnum.CommandLineParser;

    public static class MonadicCommandLineParser
    {
        public static CommandLineArguments Parse(string commandline)
        {
            return new CommandLineArguments();
        }

        static IEnumerable<ICommandLineElement> P(string commandLine)
        {
            var parser = new Magnum.CommandLineParser.MonadicCommandLineParser();
            return parser.Parse(commandLine);
        }
    }
}