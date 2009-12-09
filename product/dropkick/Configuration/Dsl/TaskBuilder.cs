namespace dropkick.Configuration.Dsl
{
    using System.Collections.Generic;
    using DeploymentModel;

    public interface TaskBuilder :
        DeploymentInspectorSite
    {
        Task ConstructTasksForServer(DeploymentServer server);
    }

    public static class TaskHelpers
    {
        public static DeploymentDetail ToDetail(this Task task, DeploymentServer server)
        {
            var d = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);

            return d;
        }
    }
}