namespace dropkick.Engine
{
    using Dsl;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }

        VerificationResult VerifyCanRun();

        void Execute();
        //dry-run
    }
}