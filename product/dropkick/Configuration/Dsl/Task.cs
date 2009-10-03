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
}