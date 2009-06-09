namespace dropkick.Dsl.CommandLine
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Engine;

    public class CommandLineTask :
        Task
    {
        public CommandLineTask(string command)
        {
            Command = command;
            FindTheCommand(command);
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get
            {
                string name = string.IsNullOrEmpty(ExecuteIn) ? "'{0} {1}' using PATH" : "{0} {1} in {2}";
                return "COMMAND LINE: " + string.Format(name, Command, Args, ExecuteIn);
            }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            
            if(!Directory.Exists(ExecuteIn))
                result.AddAlert(string.Format("Can't find the executable '{0}'", Path.Combine(ExecuteIn, Command)));

            if(LookInTheDirectory(ExecuteIn, Command))
                result.AddGood(string.Format("Found command '{0}' in '{1}'", Command, ExecuteIn));

            return result;
        }

        private void FindTheCommand(string command)
        {
            var path = Environment.GetEnvironmentVariable("PATH");
            foreach (var dir in path.Split(';'))
            {
                if(Directory.Exists(dir) && LookInTheDirectory(dir, command))
                    ExecuteIn = dir;                
            }
        }
        private bool LookInTheDirectory(string dir, string command)
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

        public void Execute()
        {
            ProcessStartInfo psi = new ProcessStartInfo(Command, Args);

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;

            using (Process p = Process.Start(psi))
            {
                //what to do here?
            }
        }

        public string Command { get; set; }
        public string Args { get; set; }
        public string ExecuteIn { get; set; }
    }
}