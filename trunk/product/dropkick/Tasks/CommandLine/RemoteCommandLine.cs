namespace dropkick.Tasks.CommandLine
{
    using System;
    using System.IO;
    using System.Management;
    using Configuration.Dsl;
    using DeploymentModel;

    public class RemoteCommandLine :
        Task
    {
        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get {
                string name = string.IsNullOrEmpty(ExecutableIsLocatedAt) ? "'{0} {1}' using PATH" : "{0} {1} in {2}";
                return "REMOTE COMMAND LINE: " + string.Format(name, Command, Args, ExecutableIsLocatedAt);
            }
        }

        public DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();

            if (!Directory.Exists(ExecutableIsLocatedAt))
                result.AddAlert(string.Format("Can't find the executable '{0}'", Path.Combine(ExecutableIsLocatedAt, Command)));

            if (IsTheExeInThisDirectory(ExecutableIsLocatedAt, Command))
                result.AddGood(string.Format("Found command '{0}' in '{1}'", Command, ExecutableIsLocatedAt));

            return result;
        }

        private bool IsTheExeInThisDirectory(string dir, string command)
        {
            if (!Directory.Exists(dir))
            {
                return false;
            }


            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                if (name.Equals(command, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public DeploymentResult Execute()
        {

            var result = new DeploymentResult();
            var connOptions = new ConnectionOptions
                                            {
                                                Impersonation = ImpersonationLevel.Impersonate, EnablePrivileges = true
                                            };

            var manScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", Machine), connOptions);
            manScope.Connect();

            var objectGetOptions = new ObjectGetOptions();
            var managementPath = new ManagementPath("Win32_Process");
            var processClass = new ManagementClass(manScope, managementPath, objectGetOptions);

            var inParams = processClass.GetMethodParameters("Create");

            inParams["CommandLine"] = Command;
            var outParams = processClass.InvokeMethod("Create", inParams, null);

            int returnVal = Convert.ToInt32(outParams["returnValue"]);

            switch (returnVal)
            {
                case 2:
                    throw new Exception("Access is denied.");

                case 3:
                    throw new Exception("Insufficient privileges.");

                case 8:
                    throw new Exception("Unknown failure.");

                case 9:
                    throw new Exception("Path not found.");

                case 21:
                    throw new Exception("Invalid parameter");
            }

            return result;
        }


        public string Command { get; set; }
        public string Args { get; set; }
        public string ExecutableIsLocatedAt { get; set; }
        public string WorkingDirectory { get; set; }
        public string Machine { get; set; }
    }
}