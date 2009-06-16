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
            //                -- /d:FHLBank.Flames.Deployment
            
            // -v to verify
            // -x to execute
            // default is trace
            var a = ArgumentParsing.Parse(args);

            new Runner(a.DeploymentFinder).Deploy(a);
        }
    }
}