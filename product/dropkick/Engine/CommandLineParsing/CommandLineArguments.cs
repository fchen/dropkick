namespace dropkick.Engine.CommandLineParsing
{
    public class CommandLineArguments
    {
        //named or 'all'
        public string PartName { get; set; }
        public string EnvironmentName { get; set; }

        //trace, execute, verify
        public string Command { get; set; }

        public string Deployment { get; set; }
    }
}