namespace dropkick.Configuration.Dsl.Iis
{
    using Tasks.Iis;

    public class Iis6TaskCfg :
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly Role _role;
        readonly Iis6Task _task;


        public Iis6TaskCfg(Server server, string websiteName)
        {
            _role = server.Role;
            _task = new Iis6Task()
                    {
                        ServerName = server.Name,
                        WebsiteName = websiteName
                    };
        }

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            _task.VdirPath = name;
            _role.AddTask(_task);
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesntExist();
        }
    }
}