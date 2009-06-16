namespace dropkick.console
{
    using Engine;

    internal class Program
    {
        static void Main(string[] args)
        {
            //get environment --- /e:local|dev|test|qa|prod|dr
            //get part/partCfg -- /p:web|app|service
            //get 'Deployment' class from assembly
            //                 -- /d:FHLBank.Flames.Deployment.FlamesDeployment (or there could just be one?)

            var a = new ExecutionArguments();

            new Runner(new AssumesOnlyOneDeploymentFinder(a.DeploymentAssembly)).Deploy(a);
        }
    }
}