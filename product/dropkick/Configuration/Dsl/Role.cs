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

    public class Role<T> :
        RoleCfg,
        DeploymentInspectorSite
        where T : Deployment<T>, new()
    {
        readonly List<Task> _tasks = new List<Task>();
        readonly IList<ServerOptions> _servers = new List<ServerOptions>();

        public Role(string name)
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

        public void BindAction(Action<ServerOptions> action)
        {
            foreach (var server in _servers)
            {
                action(server);
            }
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

        public static Role<T> GetRole(Role input)
        {
            Role<T> result = input as Role<T>;
            if(result == null)
                throw new ArgumentException(string.Format("The part is not valid for this deployment"), "input");

            return result;
        }
    }
}