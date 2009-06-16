namespace dropkick.Dsl.WinService
{
    using System.Threading;
    using Visitors.Verification;

    public abstract class BaseServiceTask : 
        Task
    {
        protected BaseServiceTask(string machineName, string serviceName)
        {
            MachineName = machineName;
            ServiceName = serviceName;
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public abstract string Name{ get;}

        public abstract VerificationResult VerifyCanRun();
        public abstract void Execute();

        public string MachineName { get; set; }
        public string ServiceName { get; set; }

        protected void VerifyInAdministratorRole(VerificationResult result)
        {
            if (Thread.CurrentPrincipal.IsInRole("Administrator"))
            {
                result.AddError("You are not in the Administrator role. you will not be able to start/stop services");
            }
            else
            {
                result.AddGood("You are in the Administrator role");
            }
        }
    }
}