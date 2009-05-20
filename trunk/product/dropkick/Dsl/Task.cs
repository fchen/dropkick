namespace dropkick.Dsl
{
    using Engine;

    public interface Task :
        DeploymentInspectorSite
    {
        string Name { get; }

        VerificationResult VerifyCanRun();

        void Execute();
        //dry-run
    }
}