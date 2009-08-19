namespace dropkick.Dsl.NetworkShare
{
    public class FolderShareTaskCfg :
        FolderShareOptions
    {
        ServerOptions _server;
        FolderShareTask _task;
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