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
            return this;
        }

        public FileAction EncryptIdentity()
        {
            return this;
        }
    }
}