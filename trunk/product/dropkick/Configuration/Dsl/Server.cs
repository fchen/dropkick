namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;
    using DeploymentModel;
    using Tasks.Msmq;

    public interface Server :
        DeploymentInspectorSite
    {
        string Name { get; }
        void MapTo(DeploymentServer server);
        void RegisterTask(Task task);
    }

    public class PrototypicalServer :
        Server
    {
        IList<TaskBuilder> _tasks = new List<TaskBuilder>();

        public string Name { get; set; }
        public void InspectWith(DeploymentInspector inspector)
        {
            inspector.Inspect(this, ()=>
            {
                foreach (TaskBuilder task in _tasks)
                {
                    task.InspectWith(inspector);
                }
            });
        }

        //TODO: YUCK
        public void MapTo(DeploymentServer server)
        {
            foreach (var task in _tasks)
            {
                server.AddDetail(task.ConstructTasksForServer(server).ToDetail(server));
            }
        }

        public void RegisterTask(Task task)
        {
            throw new NotImplementedException();
        }
    }
}