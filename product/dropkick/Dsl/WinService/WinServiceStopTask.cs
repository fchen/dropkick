namespace dropkick.Dsl.WinService
{
    using System;
    using System.ServiceProcess;
    using Engine;

    public class WinServiceStopTask :
        BaseServiceTask
    {
        public WinServiceStopTask(string machineName, string serviceName) : base(machineName, serviceName)
        {
        }

        public override string Name
        {
            get { return string.Format("Stopping Service '{0}'", ServiceName); }
        }

        public override VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            try
            {
                using (ServiceController c = new ServiceController(ServiceName, MachineName))
                {
                    var currentStatus = c.Status;
                    result.AddGood(string.Format("Found the Service it is '{0}'", currentStatus));
                }
            }
            catch (Exception)
            {
                result.AddError(string.Format("Can't Find Service '{0}'", ServiceName));
            }

            return result;
        }

        public override void Execute()
        {
            using(ServiceController c = new ServiceController(ServiceName, MachineName))
            {
                if(c.CanStop)
                    c.Stop();
                c.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(5));
            }
        }
    }
}