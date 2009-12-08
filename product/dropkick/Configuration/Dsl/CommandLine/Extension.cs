namespace dropkick.Configuration.Dsl.CommandLine
{
    public static class Extension
    {
        public static CommandLineOptions CommandLine(this Role role, string command)
        {
            foreach (var server in role.Servers)
            {
                
            if (server.IsLocal)
                return new LocalCommandLineTaskBuilder(server.Role, command);
            else
                return new RemoteCommandLineTaskBuilder(server.Role, command);
            }

            //TODO: arcgh
            return null;
        }
    }
}