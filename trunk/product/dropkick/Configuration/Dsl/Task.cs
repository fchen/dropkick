namespace dropkick.Dsl
{
    using Verification;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }
        VerificationResult VerifyCanRun();
        void Execute();
    }
}