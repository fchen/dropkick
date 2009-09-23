namespace dropkick.Engine
{
    using CommandLineParsing;
    using DeploymentFinders;

    public static class Runner
    {
        public static void Deploy(DeploymentFinder finder, ExecutionArguments args)
        {
            var deployment = finder.Find(args.DeploymentAssembly);

            var inspector = new DropkickDeploymentInspector();
            inspector.Inspect(deployment);

            var plan = inspector.GetPlan();
            plan.Execute(args);
        }

        public static void Deploy(string commandLine)
        {
            var finder = new SearchesForAnAssemblyEndingInDeployment();

            var newArgs = MonadicCommandLineParser.Parse(commandLine);

            var deployment = finder.Find(newArgs.Deployment);

            //this seems a bit ginchy
            var inspector = new DropkickDeploymentInspector();
            inspector.Inspect(deployment);
            var plan = inspector.GetPlan();

            var exArgs = new ExecutionArguments();
            plan.Execute(exArgs);
        }
    }
}