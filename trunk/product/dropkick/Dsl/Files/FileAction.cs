namespace dropkick.Dsl.Files
{
    public interface FileAction
    {
        FileAction ReplaceIdentityTokensWithPrompt();
        FileAction EncryptIdentity();
    }
}