namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class RemoteCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly PartCfg _part;
        readonly RemoteCommandLineTask _task;

        public RemoteCommandLineTaskBuilder(PartCfg part, string command)
        {
            _part = part;
            _task = new RemoteCommandLineTask(command);
            _part.AddTask(_task);
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