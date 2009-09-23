using dropkick.Execution;

namespace dropkick.Configuration.Dsl
{
    using Verification;

    public class NoteTask :
        Task
    {
        string _message;

        public NoteTask(string message)
        {
            _message = message;
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return "NOTE: {0}".FormatWith(_message); }
        }

        public VerificationResult VerifyCanRun()
        {
            return new VerificationResult();
        }

        public DeploymentResult Execute()
        {
            //do nothing
            return new DeploymentResult();
        }
    }
}