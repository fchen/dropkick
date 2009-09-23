namespace dropkick.Engine
{
    public class DeploymentArguments
    {
        public DeploymentArguments()
        {
            Command = DropkickCommands.Trace;
            Part = "ALL";
        }

        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public DropkickCommands Command { get; set; }
    }
}