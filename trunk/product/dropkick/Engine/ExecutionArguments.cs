namespace dropkick.Engine
{
    using DeploymentFinders;
    using Dsl;

    public class ExecutionArguments
    {
        public ExecutionArguments()
        {
            Command = DropkickCommands.Trace;
            DeploymentFinder = new AssemblyWasSpecifiedAssumingOnlyOneDeploymentClass();
            Part = "ALL";
        }
        public string Environment { get; set; }
        public string Part { get; set; }
        public string DeploymentAssembly { get; set; }
        public DropkickCommands Command { get; set; }
        public DeploymentFinder DeploymentFinder { get; set; }
    }

    public class Arguments
    {
        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public DropkickCommands Command { get; set; }
    }
}