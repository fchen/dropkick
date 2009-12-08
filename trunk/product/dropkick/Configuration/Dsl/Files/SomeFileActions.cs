namespace dropkick.Configuration.Dsl.Files
{
    public class SomeFileActions :
        FileActions
    {
        readonly Server _server;

        public SomeFileActions(Server server)
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