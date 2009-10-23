namespace dropkick.Configuration.Dsl.NetworkShare
{
    using Tasks.NetworkShare;

    public class FolderShareTaskCfg :
        FolderShareOptions
    {
        readonly ServerOptions _server;
        readonly FolderShareTask _task;
        public FolderShareTaskCfg(ServerOptions server, string name)
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
            _server.Part.AddTask(_task);
            return this;
        }

        public void CreateIfNotExist()
        {
            _task.CreateIfNotExist();
        }
    }
}