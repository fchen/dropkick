namespace dropkick.Configuration.Dsl.Files
{
    public class AppFileActions :
        FileAction
    {
        Server _server;

        public AppFileActions(Server server)
        {
            _server = server;
        }

        public FileAction ReplaceIdentityTokensWithPrompt()
        {
            //replace {{username}} and {{password}}?
            return this;
        }

        public FileAction EncryptIdentity()
        {
            //get DPAPI code from work.
            return this;
        }
    }
}