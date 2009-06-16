namespace dropkick.Dsl.WinService
{
    using Visitors.Verification;

    public class NestedTask :
        Task
    {
        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this, ()=>{/*other stuff*/});
        }

        public string Name
        {
            get { return "NESTED TASK"; }
        }

        public VerificationResult VerifyCanRun()
        {
            return new VerificationResult();
        }

        public void Execute()
        {
            //execute sub task
        }
    }
}