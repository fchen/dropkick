namespace dropkick.Configuration.Dsl.Files
{
    public interface FileAction
    {
        FileAction ReplaceIdentityTokensWithPrompt();
        FileAction EncryptIdentity();
    }
}