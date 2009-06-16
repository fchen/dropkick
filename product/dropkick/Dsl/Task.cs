namespace dropkick.Dsl
{
    using Visitors.Verification;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }

        VerificationResult VerifyCanRun();

        void Execute();
        //dry-run
    }
}