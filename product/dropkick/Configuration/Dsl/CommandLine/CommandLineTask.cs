namespace dropkick.Dsl.CommandLine
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Verification;

    public class CommandLineTask :
        Task
    {
        public CommandLineTask(string command)
        {
            Command = command;
            ExecutableIsLocatedAt = FindThePathToTheCommand(command);
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get
            {
                string name = string.IsNullOrEmpty(ExecutableIsLocatedAt) ? "'{0} {1}' using PATH" : "{0} {1} in {2}";
                return "COMMAND LINE: " + string.Format(name, Command, Args, ExecutableIsLocatedAt);
            }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            
            if(!Directory.Exists(ExecutableIsLocatedAt))
                result.AddAlert(string.Format("Can't find the executable '{0}'", Path.Combine(ExecutableIsLocatedAt, Command)));

            if(IsTheExeInThisDirectory(ExecutableIsLocatedAt, Command))
                result.AddGood(string.Format("Found command '{0}' in '{1}'", Command, ExecutableIsLocatedAt));

            return result;
        }

        private string FindThePathToTheCommand(string command)
        {
            var result = WorkingDirectory;

            var path = Environment.GetEnvironmentVariable("PATH");
            foreach (var dir in path.Split(';'))
            {
                if (Directory.Exists(dir) && IsTheExeInThisDirectory(dir, command))
                {
                    result = dir;
                    break;
                }
            }
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

        public void Execute()
        {
            ProcessStartInfo psi = new ProcessStartInfo(Command, Args);

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;

            if (!string.IsNullOrEmpty(WorkingDirectory)) psi.WorkingDirectory = WorkingDirectory;

            using (Process p = Process.Start(psi))
            {
                //what to do here?
            }
        }

        public string Command { get; set; }
        public string Args { get; set; }
        public string ExecutableIsLocatedAt { get; set; }
        public string WorkingDirectory { get; set; }
    }
}