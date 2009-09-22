using dropkick.Execution;

namespace dropkick.Configuration.Dsl.Dsn
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Verification;

    public class DsnTask :
        Task
    {
        [DllImport("ODBCCP32.dll")]
        private static extern bool SQLConfigDataSource(IntPtr parent, int request, string driver, string attributes);

        private string _dsnName;
        private DsnAction _action;
        private DsnDriver _driver;
        private string _serverName;
        string _databaseName;

        public DsnTask(string serverName, string dsnName, DsnAction action, DsnDriver driver, string databaseName)
        {
            _serverName = serverName;
            _databaseName = databaseName;
            _dsnName = dsnName;
            _action = action;
            _driver = driver;
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return "DSN: {0}".FormatWith(_dsnName); }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            VerifyInAdministratorRole(result);

            return result;
        }

        public ExecutionResult Execute()
        {
            var result = new ExecutionResult();

            try
            {
                bool value = SQLConfigDataSource((IntPtr)0, (int)_action, _driver.Value, "SERVER={0}\0DSN={1}\0DESCRIPTION=NewDSN\0DATABASE={2}\0TRUSTED_CONNECTION=YES".FormatWith(_serverName, _dsnName, _databaseName));
                result.AddGood("Created DSN");
            }
            catch (Exception ex)
            {
                result.AddError("Failed to create DSN", ex);
            }
            
            
            return result;
        }

        void VerifyInAdministratorRole(VerificationResult result)
        {
            if (Thread.CurrentPrincipal.IsInRole("Administrator"))
            {
                result.AddAlert("You are not in the Administrator role");
            }
            else
            {
                result.AddGood("You are in the Administrator role");
            }
        }
    }
}