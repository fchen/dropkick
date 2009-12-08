namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;
    using DeploymentModel;

    public interface Role
    {
        string Name { get; }
        void ConfigureServer(DeploymentServer server);
    }

    public class Role<T> :
        Role,
        DeploymentInspectorSite
        where T : Deployment<T>, new()
    {
        readonly List<Task> _tasks = new List<Task>();
        Action<DeploymentServer> _serverConfiguration;

        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
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

        public void BindAction(Action<DeploymentServer> action)
        {
            _serverConfiguration = action;
        }

        public void ConfigureServer(DeploymentServer server)
        {
            _serverConfiguration(server);
        }
    }
}