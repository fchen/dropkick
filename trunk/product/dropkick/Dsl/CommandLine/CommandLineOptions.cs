namespace dropkick.Dsl.CommandLine
{
    public interface CommandLineOptions
    {
        CommandLineOptions Args(string args);
        CommandLineOptions ExecuteIn(string path);
    }
}