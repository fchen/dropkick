namespace dropkick.Engine
{
    using Dsl;

    public class ExecutionArguments
    {
        public ExecutionArguments()
        {
            Option = ExecutionOptions.Trace;
            DeploymentFinder = new AssumesOnlyOneDeploymentFinder();
            Part = "ALL";
        }
        public string Environment { get; set; }
        public string Part { get; set; }
        public string DeploymentAssembly { get; set; }
        public ExecutionOptions Option { get; set; }
        public DeploymentFinder DeploymentFinder { get; set; }
    }

    public class Arguments
    {
        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public ExecutionOptions Option { get; set; }
    }
}