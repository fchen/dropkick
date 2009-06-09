namespace dropkick.Dsl.CommandLine
{
    using System;

    public class CommandLineTaskBuilder : 
        CommandLineOptions
    {
        private Part _part;
        private CommandLineTask _task;

        public CommandLineTaskBuilder(Part part, string command)
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

        public CommandLineOptions ExecuteIn(string path)
        {
            _task.ExecuteIn = path;
            return this;
        }
    }
}