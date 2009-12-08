namespace dropkick.Configuration.Dsl.Files
{
    public class AppFileActions :
        FileAction
    {
        Role _role;

        public AppFileActions(Role role)
        {
            _role = role;
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