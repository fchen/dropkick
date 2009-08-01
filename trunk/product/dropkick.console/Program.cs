namespace dropkick.console
{
    using Engine;

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

            var a = ArgumentParsing.Parse(args);
            var r = new Runner(a.DeploymentFinder);
            r.Deploy(a);
        }
    }
}