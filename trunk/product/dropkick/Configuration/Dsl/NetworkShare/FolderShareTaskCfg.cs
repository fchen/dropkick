namespace dropkick.Configuration.Dsl.NetworkShare
{
    using Tasks.NetworkShare;

    public class FolderShareTaskCfg :
        FolderShareOptions
    {
        readonly FolderShareTask _task;
        readonly Role _role;
        public FolderShareTaskCfg(Role role, string name)
        {
            _role = role;
            _task = new FolderShareTask()
                    {
                        Server = role.Name,
                        ShareName = name,
                    };
        }

        public FolderShareOptions PointingTo(string path)
        {
            _task.PointingTo = path;
            _role.AddTask(_task);
            return this;
        }

        public void CreateIfNotExist()
        {
            _task.CreateIfNotExist();
        }
    }
}