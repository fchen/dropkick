namespace dropkick.Engine
{
    using Magnum.Collections;

    public class DeploymentArguments
    {
        public DeploymentArguments()
        {
            Command = DeploymentCommands.Trace;
            Part = "ALL";
            ServerMappings = new MultiDictionary<string, string>(true);
        }

        public string Environment { get; set; }
        public string Part { get; set; }
        public string Deployment { get; set; }
        public DeploymentCommands Command { get; set; }
        public MultiDictionary<string, string> ServerMappings { get; private set; }
    }
}