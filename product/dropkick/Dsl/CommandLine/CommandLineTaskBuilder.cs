namespace dropkick.Dsl.CommandLine
{
    using System;

    public class CommandLineTaskBuilder : 
        CommandLineOptions
    {
        private PartCfg _part;
        private CommandLineTask _task;

        public CommandLineTaskBuilder(PartCfg part, string command)
        {
            _part = part;
            _task = new CommandLineTask(command);
            _part.AddTask(_task);
        }

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
    }
}