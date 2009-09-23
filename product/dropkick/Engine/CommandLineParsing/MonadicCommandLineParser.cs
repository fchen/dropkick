namespace dropkick.Engine.CommandLineParsing
{
    using System.Collections.Generic;
    using Magnum.CommandLineParser;

    public static class MonadicCommandLineParser
    {
        public static DeploymentArguments Parse(string commandline)
        {
            return new DeploymentArguments();
        }

        static IEnumerable<ICommandLineElement> P(string commandLine)
        {
            var parser = new Magnum.CommandLineParser.MonadicCommandLineParser();
            return parser.Parse(commandLine);
        }
    }
}