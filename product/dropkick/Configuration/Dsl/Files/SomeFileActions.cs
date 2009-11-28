namespace dropkick.Configuration.Dsl.Files
{
    public class SomeFileActions :
        FileActions
    {
        readonly ServerOptions _server;

        public SomeFileActions(ServerOptions server)
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