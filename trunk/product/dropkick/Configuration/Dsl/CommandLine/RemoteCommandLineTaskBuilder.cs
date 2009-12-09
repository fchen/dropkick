namespace dropkick.Configuration.Dsl.CommandLine
{
    using DeploymentModel;
    using Tasks.CommandLine;

    public class RemoteCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly DeploymentServer _server;
        readonly RemoteCommandLineTask _task;

        public RemoteCommandLineTaskBuilder(DeploymentServer server, string command)
        {
            _server = server;
            _task = new RemoteCommandLineTask(command);
            _server.AddDetail(_task.ToDetail(server));
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