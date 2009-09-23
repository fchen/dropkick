using dropkick.Execution;

namespace dropkick.Configuration.Dsl
{
    

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }
        DeploymentResult VerifyCanRun();
        DeploymentResult Execute();
    }
}