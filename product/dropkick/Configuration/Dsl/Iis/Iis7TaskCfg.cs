namespace dropkick.Configuration.Dsl.Iis
{
    using Tasks.Iis;

    public class Iis7TaskCfg : 
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly Iis7Task _task;

        public Iis7TaskCfg(ServerOptions server, string websiteName)
        {
            _task = new Iis7Task(){WebsiteName = websiteName,ServerName = server.Name};
            server.Role.AddTask(_task);
            
        }

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            _task.VdirPath = name;
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesntExist();
        }
    }
}