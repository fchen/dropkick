namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class LocalCommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly PartCfg _part;
        readonly LocalCommandLineTask _task;

        public LocalCommandLineTaskBuilder(PartCfg part, string command)
        {
            _part = part;
            _task = new LocalCommandLineTask(command);
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