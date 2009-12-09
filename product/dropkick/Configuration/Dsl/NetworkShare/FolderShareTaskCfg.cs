namespace dropkick.Configuration.Dsl.NetworkShare
{
    using DeploymentModel;
    using Tasks.NetworkShare;

    public class FolderShareTaskCfg :
        FolderShareOptions
    {
        readonly DeploymentServer _server;
        readonly FolderShareTask _task;
        public FolderShareTaskCfg(DeploymentServer server, string name)
        {
            _server = server;
            _task = new FolderShareTask()
                    {
                        Server = server.Name,
                        ShareName = name,
                    };
        }

        public FolderShareOptions PointingTo(string path)
        {
            _task.PointingTo = path;
            _server.AddDetail(_task.ToDetail(_server));
            return this;
        }

        public void CreateIfNotExist()
        {
            _task.CreateIfNotExist();
        }
    }
}