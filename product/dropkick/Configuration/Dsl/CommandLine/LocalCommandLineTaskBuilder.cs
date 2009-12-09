namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class LocalCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly Server _server;
        readonly LocalCommandLineTask _task;

        public LocalCommandLineTaskBuilder(Server server, string command)
        {
            _server = server;
            _task = new LocalCommandLineTask(command);
            _server.RegisterTask(_task);
        }

        #region CommandLineOptions Members

        public CommandLineOptions Args(string args)
        {
            _task.Args = args;
            return this;
        }

        public CommandLineOptions ExecutableIsLocatedAt(string path)
        {
            _task.ExecutableIsLocatedAt = path;
            return this;
        }

        public CommandLineOptions WorkingDirectory(string path)
        {
            _task.WorkingDirectory = path;
            return this;
        }

        #endregion
    }
}