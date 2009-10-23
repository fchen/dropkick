namespace dropkick.Configuration.Dsl.CommandLine
{
    using Tasks.CommandLine;

    public class CommandLineTaskBuilder :
        CommandLineOptions
    {
        readonly PartCfg _part;
        readonly CommandLineTask _task;

        public CommandLineTaskBuilder(PartCfg part, string command)
        {
            _part = part;
            _task = new CommandLineTask(command);
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