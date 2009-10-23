namespace dropkick.Tasks
{
    using Configuration.Dsl;
    using DeploymentModel;

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

        public DeploymentResult VerifyCanRun()
        {
            return new DeploymentResult();
        }

        public DeploymentResult Execute()
        {
            //do nothing
            return new DeploymentResult();
        }
    }
}