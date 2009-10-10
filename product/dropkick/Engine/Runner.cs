namespace dropkick.Engine
{
    using CommandLineParsing;
    using DeploymentFinders;

    public static class Runner
    {
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

            var plan = DeploymentPlanBuilder.Build(deployment, newArgs);

            plan.Execute();
        }
    }
}