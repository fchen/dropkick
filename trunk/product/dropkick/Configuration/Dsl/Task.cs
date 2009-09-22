using dropkick.Execution;

namespace dropkick.Configuration.Dsl
{
    using Verification;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }
        VerificationResult VerifyCanRun();
        ExecutionResult Execute();
    }
}