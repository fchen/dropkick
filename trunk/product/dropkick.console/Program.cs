namespace dropkick.console
{
    using System;
    using Engine;
    using Magnum.CommandLineParser;

    internal class Program
    {
        static void Main(string[] args)
        {
            // get environment -- /e:local|dev|test|qa|prod|dr
            // get part        -- /p:web|app|service
            // get 'Deployment' class from assembly
            // assumes only one deployment class
            //                 -- /a:FHLBank.Flames.Deployment (an assembly)
            // specific deployment ? 
            //                 -- /d:FHLBank.Flames.Deployment.StandardDeploy (a class)

            // -v to verify
            // -x to execute
            // default is trace

            //dk execute -e:local -p:web -a:FHLBank.Flames.Deployment
            var a = ArgumentParsing.Parse(args);
            var r = new Runner(a.DeploymentFinder);
            r.Deploy(a);
        }
    }
}