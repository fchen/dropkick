namespace dropkick.Configuration.Dsl.Iis
{
    using Tasks.Iis;

    public class Iis6TaskCfg :
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly PartCfg _part;
        readonly Iis6Task _task;


        public Iis6TaskCfg(ServerOptions server, string websiteName)
        {
            _part = server.Part;
            _task = new Iis6Task()
                    {
                        ServerName = server.Name,
                        WebsiteName = websiteName
                    };
        }

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            _task.VdirPath = name;
            _part.AddTask(_task);
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesntExist();
        }
    }
}