namespace dropkick.Engine
{
    public class DeploymentArguments
    {
        public DeploymentArguments()
        {
            Command = DeploymentCommands.Trace;
            Part = "ALL";
        }

        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public DeploymentCommands Command { get; set; }
    }
}