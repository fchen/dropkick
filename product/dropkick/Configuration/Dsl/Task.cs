namespace dropkick.Configuration.Dsl
{
    using DeploymentModel;


    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }
        DeploymentResult VerifyCanRun();
        DeploymentResult Execute();
    }
}