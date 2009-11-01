namespace dropkick.Tasks.CommandLine
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Configuration.Dsl;
    using DeploymentModel;
    using Magnum.DateTimeExtensions;
    

    public class LocalCommandLineTask :
        Task
    {
        public LocalCommandLineTask(string command)
        {
            WorkingDirectory = Environment.CurrentDirectory;
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

        public DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();
            
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

        public DeploymentResult Execute()
        {
            var result = new DeploymentResult();

            ProcessStartInfo psi = new ProcessStartInfo(Command, Args);

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;

            if (!string.IsNullOrEmpty(WorkingDirectory)) psi.WorkingDirectory = WorkingDirectory;

            string output;
            using (Process p = Process.Start(psi))
            {
                //what to do here?
                p.WaitForExit(30.Seconds().Milliseconds);
                output = p.StandardOutput.ReadToEnd();
            }

            result.AddGood("Command Line Executed");

            return result;
        }

        public string Command { get; set; }
        public string Args { get; set; }
        public string ExecutableIsLocatedAt { get; set; }
        public string WorkingDirectory { get; set; }
    }
}