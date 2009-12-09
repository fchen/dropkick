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
        readonly List<TaskBuilder> _tasks = new List<TaskBuilder>();
        Action<DeploymentServer> _serverConfiguration;

        public Role(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public void AddTask(TaskBuilder taskBuilder)
        {
            _tasks.Add(taskBuilder);
        }

        public void InspectWith(DeploymentInspector inspector)
        {
            inspector.Inspect(this, () =>
            {
                foreach(TaskBuilder task in _tasks)
                {
                    task.InspectWith(inspector);
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