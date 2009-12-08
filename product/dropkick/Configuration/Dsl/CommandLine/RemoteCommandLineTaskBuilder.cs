namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class RemoteCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly Server _server;
        readonly RemoteCommandLineTask _task;

        public RemoteCommandLineTaskBuilder(Server server, string command)
        {
            _server = server;
            _task = new RemoteCommandLineTask(command);
            _server.Role.AddTask(_task);
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