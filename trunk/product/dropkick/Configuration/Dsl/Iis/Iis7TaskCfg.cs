namespace dropkick.Configuration.Dsl.Iis
{
    using DeploymentModel;
    using Tasks.Iis;

    public class Iis7TaskCfg : 
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly Iis7Task _task;

        public Iis7TaskCfg(DeploymentServer server, string websiteName)
        {
            _task = new Iis7Task(){WebsiteName = websiteName,ServerName = server.Name};
            server.AddDetail(_task.ToDetail(server));
            
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