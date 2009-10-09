namespace dropkick.Engine
{
    using CommandLineParsing;
    using Configuration.Dsl;
    using DeploymentFinders;

    public static class Runner
    {
        public static void Deploy(DeploymentFinder finder, DeploymentArguments args)
        {
            var deployment = finder.Find(args.Deployment);

            var inspector = new DropkickDeploymentInspector();
            inspector.Inspect(deployment);

            var plan = inspector.GetPlan();
            plan.Execute(args);
        }

        public static void Deploy(string commandLine)
        {
            var finder = new SearchesForAnAssemblyEndingInDeployment();

            var newArgs = DropkickCommandLineParser.Parse(commandLine);
            var deployment = finder.Find(newArgs.Deployment);

            var plan = DeploymentPlanBuilder.Build(deployment);

            var exArgs = new DeploymentArguments();
            plan.Execute(exArgs);
        }
    }
}