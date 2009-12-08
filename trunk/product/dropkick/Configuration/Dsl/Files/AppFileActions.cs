namespace dropkick.Configuration.Dsl.Files
{
    using DeploymentModel;

    public class AppFileActions :
        FileAction
    {
        DeploymentServer _server;

        public AppFileActions(DeploymentServer server)
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