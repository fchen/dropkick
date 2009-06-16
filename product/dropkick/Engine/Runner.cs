namespace dropkick.Engine
{
    public class Runner
    {
        readonly DeploymentFinder _finder;

        public Runner(DeploymentFinder finder)
        {
            _finder = finder;
        }

        public void Deploy(ExecutionArguments args)
        {
            var d = _finder.Find(args.DeploymentAssembly);

            d.Inspect(args.Inspector);
        }
    }
}