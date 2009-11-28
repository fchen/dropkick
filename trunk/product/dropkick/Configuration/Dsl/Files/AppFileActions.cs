namespace dropkick.Configuration.Dsl.Files
{
    public class AppFileActions :
        FileAction
    {
        ServerOptions _server;

        public AppFileActions(ServerOptions server)
        {
            _server = server;
        }

        public FileAction ReplaceIdentityTokensWithPrompt()
        {
            return this;
        }

        public FileAction EncryptIdentity()
        {
            return this;
        }
    }
}