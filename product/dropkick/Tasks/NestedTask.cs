namespace dropkick.Tasks
{
    using Configuration.Dsl;
    using DeploymentModel;


    public class NestedTask :
        Task
    {
        public string Name
        {
            get { return "NESTED TASK"; }
        }

        public DeploymentResult VerifyCanRun()
        {
            return new DeploymentResult();
        }

        public DeploymentResult Execute()
        {
            //execute sub task
            return new DeploymentResult();
        }
    }
}