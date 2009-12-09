namespace dropkick.Configuration.Dsl.CommandLine
{
    using System;

    public interface CommandLineOptions
    {
        CommandLineOptions Args(string args);
        CommandLineOptions ExecutableIsLocatedAt(string path);
        CommandLineOptions WorkingDirectory(string path);
    }
}