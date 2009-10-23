namespace dropkick.Engine
{
    using DeploymentFinders;

    public static class Runner
    {
        public static void Deploy(string commandLine)
        {
            var newArgs = DeploymentCommandLineParser.Parse(commandLine);
            
            //what is the better way to state this?
            DeploymentFinder finder = newArgs.Deployment == "SEARCH" ? 
                new SearchesForAnAssemblyEndingInDeployment() :
                    newArgs.Deployment.EndsWith(".dll") ?
                        new AssemblyWasSpecifiedAssumingOnlyOneDeploymentClass() :
                        (DeploymentFinder)new TypeWasSpecifiedAssumingItHasADefaultConstructor();


            var deployment = finder.Find(newArgs.Deployment);

            DeploymentPlanRunner.Build(deployment, newArgs);
        }
    }
}