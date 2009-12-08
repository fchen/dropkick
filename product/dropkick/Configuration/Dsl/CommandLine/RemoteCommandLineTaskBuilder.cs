namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class RemoteCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly Role _role;
        readonly RemoteCommandLineTask _task;

        public RemoteCommandLineTaskBuilder(Role role, string command)
        {
            _role = role;
            _task = new RemoteCommandLineTask(command);
            _role.AddTask(_task);
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