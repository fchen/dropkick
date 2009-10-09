namespace dropkick.console
{
    using System;
    using Engine;
    using Engine.CommandLineParsing;
    using Engine.DeploymentFinders;
    using Magnum.CommandLineParser;

    internal class Program
    {
        static void Main(string[] args)
        {
            // commands 
            //   verify     v
            //   execute    e
            //   trace      t   (default)

            // args
            //   environment    /e:local - used to work with config files
            //   part           /p:all|web - a bastardized capistrano roles
            //   deployment
            //      /d:FHLBank.Flames.Deployment.dll (an assembly)
            //      /d:FHLBank.Flames.Deployment.StandardDepoy (a class, lack of .dll)
            //      (null) - if omitted search for a dll ending with 'Deployment' then pass that name in

            //dk execute -e:local -p:web -a:FHLBank.Flames.Deployment
            var a = DropkickCommandLineParser.Parse(Environment.CommandLine);

            //need a better way to do finding
            var finder = new AssemblyWasSpecifiedAssumingOnlyOneDeploymentClass();
            Runner.Deploy(finder, a);

            //goal
            Runner.Deploy(Environment.CommandLine);
        }
    }
}