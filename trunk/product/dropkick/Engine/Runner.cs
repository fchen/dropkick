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
            var newArgs = DropkickCommandLineParser.Parse(commandLine);
            
            //how much more complicated can I make this?
            DeploymentFinder finder = newArgs.Deployment == "SEARCH" ? 
                new SearchesForAnAssemblyEndingInDeployment() :
                    newArgs.Deployment.EndsWith(".dll") ?
                        new AssemblyWasSpecifiedAssumingOnlyOneDeploymentClass() :
                        (DeploymentFinder)new TypeWasSpecifiedAssumingItHasADefaultConstructor();

            var deployment = finder.Find(newArgs.Deployment);

            var plan = DeploymentPlanBuilder.Build(deployment);

            var exArgs = new DeploymentArguments();
            plan.Execute(exArgs);
        }
    }
}