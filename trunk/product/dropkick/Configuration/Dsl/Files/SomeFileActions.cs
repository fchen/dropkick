namespace dropkick.Configuration.Dsl.Files
{
    using DeploymentModel;

    public class SomeFileActions :
        FileActions
    {
        readonly DeploymentServer _server;

        public SomeFileActions(DeploymentServer server)
        {
            _server = server;
        }

        public FileAction WebConfig
        {
            get { return new WebFileActions(_server); }
        }

        public FileAction AppConfig
        {
            get { return new AppFileActions(_server); }
        }
    }
}