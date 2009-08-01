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
            var deployment = _finder.Find(args.DeploymentAssembly);

            var inspector = new Inspector();
            inspector.Inspect(deployment);

            var plan = inspector.GetPlan();
            plan.Execute(args);
        }
    }
}