namespace dropkick.Dsl.CommandLine
{
    public interface CommandLineOptions
    {
        CommandLineOptions Args(string args);
        CommandLineOptions ExecutableIsLocatedAt(string path);
        CommandLineOptions WorkingDirectory(string path);
    }
}