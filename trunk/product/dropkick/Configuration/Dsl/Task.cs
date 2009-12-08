namespace dropkick.Configuration.Dsl
{
    using DeploymentModel;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }

        //Change to Func<DeploymentResult>
        DeploymentResult VerifyCanRun();

        //Change to Func<DeploymentResult>
        DeploymentResult Execute();
    }

    public static class TaskHelpers
    {
        public static DeploymentDetail ToDetail(this Task task)
        {
            return new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
        }
    }
}