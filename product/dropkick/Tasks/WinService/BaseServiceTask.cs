namespace dropkick.Tasks.WinService
{
    using System.Threading;
    using Configuration.Dsl;
    using Execution;
    

    public abstract class BaseServiceTask :
        Task
    {
        protected BaseServiceTask(string machineName, string serviceName)
        {
            MachineName = machineName;
            ServiceName = serviceName;
        }

        public string MachineName { get; set; }
        public string ServiceName { get; set; }

        #region Task Members

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public abstract string Name { get; }

        public abstract DeploymentResult VerifyCanRun();
        public abstract DeploymentResult Execute();

        #endregion

        protected void VerifyInAdministratorRole(DeploymentResult result)
        {
            if (Thread.CurrentPrincipal.IsInRole("Administrator"))
            {
                result.AddAlert("You are not in the 'Administrator' role. You will not be able to start/stop services");
            }
            else
            {
                result.AddGood("You are in the 'Administrator' role");
            }
        }
    }
}