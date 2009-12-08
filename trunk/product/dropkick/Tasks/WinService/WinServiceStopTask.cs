namespace dropkick.Tasks.WinService
{
    using System;
    using System.ServiceProcess;
    using Configuration.Dsl;
    using DeploymentModel;


    public class WinServiceStopTask :
        BaseServiceTask
    {
        public WinServiceStopTask(Role role, string serviceName) :
            base(role, serviceName)
        {
        }

        public override string Name
        {
            get { return string.Format("Stopping service '{0}'", ServiceName); }
        }

        public override DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();

            VerifyInAdministratorRole(result);

            try
            {
                string machineName = "FIX"; //TODO: Fix this
                using (var c = new ServiceController(ServiceName, machineName))
                {
                    ServiceControllerStatus currentStatus = c.Status;
                    result.AddGood(string.Format("Found service '{0}' it is '{1}'", ServiceName, currentStatus));
                }
            }
            catch (Exception)
            {
                result.AddAlert(string.Format("Can't find service '{0}'", ServiceName));
            }

            return result;
        }

        public override DeploymentResult Execute()
        {
            string machineName = "FIX"; //TODO: Fix this
            using (var c = new ServiceController(ServiceName, machineName))
            {
                if (c.CanStop)
                    c.Stop();
                c.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(5));
            }

            return new DeploymentResult();
        }
    }
}