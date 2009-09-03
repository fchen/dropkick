namespace dropkick.Configuration.Dsl.Files
{
    using dropkick.Dsl.Files;

    public class AppFileActions :
        FileAction
    {
        Part _part;

        public AppFileActions(Part part)
        {
            _part = part;
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