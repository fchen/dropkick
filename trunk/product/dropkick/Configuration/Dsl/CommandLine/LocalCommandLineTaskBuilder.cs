namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class LocalCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly RoleCfg _role;
        readonly LocalCommandLineTask _task;

        public LocalCommandLineTaskBuilder(RoleCfg role, string command)
        {
            _role = role;
            _task = new LocalCommandLineTask(command);
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