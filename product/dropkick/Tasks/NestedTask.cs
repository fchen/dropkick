namespace dropkick.Tasks
{
    using Configuration.Dsl;
    using Execution;
    

    public class NestedTask :
        Task
    {
        #region Task Members

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this, () =>
                                        {
/*other stuff*/
                                        });
        }

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

        #endregion
    }
}