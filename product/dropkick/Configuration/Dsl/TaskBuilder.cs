namespace dropkick.Configuration.Dsl
{
    using System.Collections.Generic;
    using DeploymentModel;

    public interface TaskBuilder :
        DeploymentInspectorSite
    {
        Task[] ConstructTasksForServer(DeploymentServer server);
    }

    public static class TaskHelpers
    {
        public static DeploymentDetail[] ToDetail(this TaskBuilder taskBuilder, DeploymentServer server)
        {
            var result = new List<DeploymentDetail>();
            foreach (var task in taskBuilder.ConstructTasksForServer(server))
            {
                result.Add(new DeploymentDetail(()=>task.Name, task.VerifyCanRun, task.Execute));
            }
            return result.ToArray();
        }

        public static DeploymentDetail[] ToDetail(this Task task, DeploymentServer server)
        {
            var d = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);

            return new[] {d};
        }
    }
}