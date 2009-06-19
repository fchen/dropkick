namespace dropkick.Engine
{
    using Dsl;
    using Visitors.Trace;

    public class ExecutionArguments
    {
        public ExecutionArguments()
        {
            Inspector = new TraceVisitor();
            DeploymentFinder = new AssumesOnlyOneDeploymentFinder();
            Part = "ALL";
        }
        public string Environment { get; set; }
        public string Part { get; set; }
        public string DeploymentAssembly { get; set; }
        public DeploymentInspector Inspector { get; set; }
        public DeploymentFinder DeploymentFinder { get; set; }
    }
}