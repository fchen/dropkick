namespace dropkick.Dsl.Dsn
{
    using System;
    using System.Runtime.InteropServices;
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

        public DsnTask(string serverName, string dsnName, DsnAction action, DsnDriver driver)
        {
            _serverName = serverName;
            _dsnName = dsnName;
            _action = action;
            _driver = driver;
        }

        public void Inspect(DeploymentInspector inspector)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return "DSN: {0}".FormatWith(_dsnName); }
        }

        public VerificationResult VerifyCanRun()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            bool value =  SQLConfigDataSource((IntPtr) 0, (int)_action, "DRIVER", "ATTRIBUTES");
        }
    }
}