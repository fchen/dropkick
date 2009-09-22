using dropkick.Execution;

namespace dropkick.Configuration.Dsl.WinService
{
    using Verification;

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

        public ExecutionResult Execute()
        {
            //execute sub task
            return new ExecutionResult();
        }
    }
}