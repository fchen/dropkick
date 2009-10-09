namespace dropkick.Engine.CommandLineParsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Magnum.CommandLineParser;

    public static class MonadicCommandLineParser
    {
        public static DeploymentArguments Parse(string commandline)
        {
            var result = new DeploymentArguments();

            Set(result, P(commandline));

            return new DeploymentArguments();
        }

        static void Set(DeploymentArguments arguments, IEnumerable<ICommandLineElement> commandLineElements)
        {
            var command = commandLineElements.Where(x => x is IArgumentElement)
                .Select(x => (IArgumentElement) x)
                .DefaultIfEmpty(new ArgumentElement("trace"))
                .SingleOrDefault();

            arguments.Command = command.Id.ToEnum<DropkickCommands>();

            
            var deployment = commandLineElements.Where(x => x is IDefinitionElement)
                .Select(x => x as IDefinitionElement)
                .Where(x => x.Key == "d")
                .DefaultIfEmpty(new DefinitionElement("d","value"))
                .SingleOrDefault();

            arguments.Deployment = deployment.Value;


            var part = commandLineElements.Where(x => x is IDefinitionElement)
                .Select(x => x as IDefinitionElement)
                .Where(x => x.Key == "p")
                .DefaultIfEmpty(new DefinitionElement("p", "ALL"))
                .SingleOrDefault();

            arguments.Part = part.Value;


        }

        static IEnumerable<ICommandLineElement> P(string commandLine)
        {
            var parser = new Magnum.CommandLineParser.MonadicCommandLineParser();

            return parser.Parse(commandLine);
        }
    }
}