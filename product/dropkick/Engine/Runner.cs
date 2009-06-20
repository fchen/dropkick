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
            var interpreter = new Interpreter();
            interpreter.Inspect(deployment);
            var plan = interpreter.GetPlan();
            plan.Execute(args);
        }
    }
}