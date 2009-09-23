namespace dropkick.Engine.CommandLineParsing
{
    using System.Collections.Generic;
    using Magnum.CommandLineParser;

    public class MonadicCommandLineParser
    {
        
        public void Parse()
        {
            
        }

        private IEnumerable<ICommandLineElement> P(string commandLine)
        {
            var parser = new Magnum.CommandLineParser.MonadicCommandLineParser();
            return parser.Parse(commandLine);
        }
    }
}