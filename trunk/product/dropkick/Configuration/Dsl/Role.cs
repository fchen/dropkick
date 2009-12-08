namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;
    using DeploymentModel;

    public interface Role
    {
        string Name { get; }
        void AddTask(Task task);
    }

    public class Role<T> :
        Role,
        DeploymentInspectorSite
        where T : Deployment<T>, new()
    {
        readonly List<Task> _tasks = new List<Task>();
        readonly IList<DeploymentServer> _servers = new List<DeploymentServer>();

        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }

        public void BindAction(Action<DeploymentServer> action)
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