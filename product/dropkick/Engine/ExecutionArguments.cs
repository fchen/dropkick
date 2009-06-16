namespace dropkick.Engine
{
    using Dsl;

    public class ExecutionArguments
    {
        public string Environment { get; set; }
        public string Part { get; set; }
        public string DeploymentAssembly { get; set; }
        public DeploymentInspector Inspector { get; set; }
    }
}