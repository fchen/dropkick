namespace dropkick.Dsl.Files
{
    using System;

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