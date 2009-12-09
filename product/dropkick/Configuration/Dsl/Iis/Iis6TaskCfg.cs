namespace dropkick.Configuration.Dsl.Iis
{
    using DeploymentModel;
    using Tasks.Iis;

    public class Iis6TaskCfg :
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly Iis6Task _task;
        readonly DeploymentServer _server;

        public Iis6TaskCfg(DeploymentServer server, string websiteName)
        {
            _server = server;
            _task = new Iis6Task()
                    {
                        ServerName = server.Name,
                        WebsiteName = websiteName
                    };
            server.AddDetail(_task.ToDetail(server));
        }

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            _task.VdirPath = name;
            _server.AddDetail(_task.ToDetail(_server));
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesntExist();
        }
    }
}