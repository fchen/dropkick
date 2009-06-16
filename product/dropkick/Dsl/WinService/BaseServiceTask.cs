namespace dropkick.Dsl.WinService
{
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
    }
}