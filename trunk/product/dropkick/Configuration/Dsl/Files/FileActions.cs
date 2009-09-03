namespace dropkick.Configuration.Dsl.Files
{
    public interface FileActions
    {
        FileAction WebConfig { get; }
        FileAction AppConfig { get; }
    }
}