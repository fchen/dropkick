namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;
    using dropkick.Dsl;

    public interface Part
    {
        string Name { get; }
        ServerOptions OnServer(string serverName);
    }

    public interface PartCfg : Part
    {
        void AddTask(Task task);
    }

    public class Part<T> :
        PartCfg,
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

        public void BindAction(Action<Part> action)
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

        public static Part<T> GetPart(Part input)
        {
            Part<T> result = input as Part<T>;
            if(result == null)
                throw new ArgumentException(string.Format("The part is not valid for this deployment"), "input");

            return result;
        }
    }
}