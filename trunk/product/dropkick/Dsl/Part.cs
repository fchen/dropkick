namespace dropkick.Dsl
{
    using System;
    using System.Collections.Generic;

    public interface Part
    {
        string Name { get; }
        ServerOptions OnServer(string serverName);
        void AddTask(Task task);
    }

    public class Part<T> :
        Part,
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

        public static Part<T> GetPart(Part input)
        {
            Part<T> result = input as Part<T>;
            if(result == null)
                throw new ArgumentException(string.Format("The part is not valid for this deployment"), "input");

            return result;
        }

        public ServerOptions OnServer(string serverName)
        {
            return new ServerOptions(serverName, this);
        }

        public void BindAction(Action<Part> action)
        {
            action(this);
        }

        public void Execute()
        {
            foreach(Task task in _tasks)
            {
                task.Execute();
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
    }
}