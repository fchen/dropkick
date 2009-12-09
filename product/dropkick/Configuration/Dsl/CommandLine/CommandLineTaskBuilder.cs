namespace dropkick.Configuration.Dsl.CommandLine
{
    using DeploymentModel;
    using Tasks.CommandLine;

    public class CommandLineTaskBuilder :
        CommandLineOptions,
        TaskBuilder
    {
        string _command;
        string _args;
        string _path;
        string _workingDirectory;

        public CommandLineTaskBuilder(string command)
        {
            _command = command;
        }

        public CommandLineOptions Args(string args)
        {
            _args = args;
            return this;
        }

        public CommandLineOptions ExecutableIsLocatedAt(string path)
        {
            _path = path;
            return this;
        }

        public CommandLineOptions WorkingDirectory(string path)
        {
            _workingDirectory = path;
            return this;
        }

        public void InspectWith(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public Task ConstructTasksForServer(DeploymentServer server)
        {
            if (server.IsLocal)
                return new LocalCommandLineTask(_command)
                       {
                           Args = _args,
                           ExecutableIsLocatedAt =  _path,
                           WorkingDirectory = _workingDirectory
                       };
            else
                return new RemoteCommandLineTask(_command)
                       {
                           Args = _args,
                           ExecutableIsLocatedAt = _path,
                           WorkingDirectory = _workingDirectory
                       };
        }
    }
}