namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;

    public interface Role
    {
        string Name { get; }
        ServerOptions OnServer(string serverName);
        void OnServer(string serverName, Action<ServerOptions> server);
    }

    public interface RoleCfg : Role
    {
        void AddTask(Task task);
    }

    public class Part<T> :
        RoleCfg,
        DeploymentInspectorSite
        where T : Deployment<T>, new()
    {
        readonly List<Task> _tasks = new List<Task>();

        public Part(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }


        public ServerOptions OnServer(string serverName)
        {
            return new ServerOptions(serverName, this);
        }

        public void OnServer(string serverName, Action<ServerOptions> server)
        {
            var so = new ServerOptions(serverName, this);
            server(so);
        }

        public void BindAction(Action<Role> action)
        {
            action(this);
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this, () =>
            {
                foreach(Task task in _tasks)
                {
                    task.Inspect(inspector);
                }
            });
        }

        public static Part<T> GetPart(Role input)
        {
            Part<T> result = input as Part<T>;
            if(result == null)
                throw new ArgumentException(string.Format("The part is not valid for this deployment"), "input");

            return result;
        }
    }
}