namespace dropkick.Engine
{
    using Magnum.Collections;

    public class DeploymentArguments
    {
        public DeploymentArguments()
        {
            Command = DeploymentCommands.Trace;
            Part = "ALL";
            ServerMappings = new RoleToServerMap();
        }

        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public DeploymentCommands Command { get; set; }
        public RoleToServerMap ServerMappings { get; private set; }
    }
}