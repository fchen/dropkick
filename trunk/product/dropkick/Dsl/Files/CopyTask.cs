namespace dropkick.Dsl.Files
{
    using System;
    using System.IO;
    using Visitors.Verification;

    public class CopyTask :
        Task
    {
        string _from;
        string _to;
        Action<FileActions> _followOnAction = (fa) => { };

        public CopyTask(string @from, string to)
        {
            _from = from;
            _to = to;
        }

        public string Name
        {
            get { return string.Format("Copy '{0}' to '{1}'", _from, _to); }
        }

        public void SetFollowOnAction(Action<FileActions> actions)
        {
            _followOnAction = actions;
        }

        public void Execute()
        {
            //check is valid from path
            _from = Path.GetFullPath(_from);

            //check is valid to path
            _to = Path.GetFullPath(_to);

            //todo: verify that from exists
            if(!Directory.Exists(_to))
            {
                Directory.CreateDirectory(_to);
            }

            if (Directory.Exists(_from))
            {
                foreach (var file in Directory.GetFiles(_from))
                {
                    //need to support recursion
                    var fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(_to, fileName));
                    //log file was copied / event?
                }

                //what do you want to do if the directory DOESN'T exist?
            }

        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            //check is valid from path
            _from = Path.GetFullPath(_from);

            //check is valid to path
            _to = Path.GetFullPath(_to);

            //check can write from _to
            if (!Directory.Exists(_to))
                result.AddAlert(string.Format("'{0}' doesn't exist and will be created", _to));

            if (Directory.Exists(_from))
            {
                result.AddGood(string.Format("'{0}' exists", _from));
                //check can read from _from
                var readFiles = Directory.GetFiles(_from);
                foreach (string file in readFiles)
                {
                    Stream fs = new MemoryStream();
                    try
                    {
                        fs = File.Open(file, FileMode.Open, FileAccess.Read);
                        result.AddGood(string.Format("Going to copy '{0}' to '{1}'", file, _to));
                    }
                    catch (Exception)
                    {
                        result.AddError("CopyTask: Can't read file '{0}'");
                    }
                    finally
                    {
                        fs.Dispose();
                    }
                }
            }
            else
            {
                result.AddAlert(string.Format("'{0}' doesn't exist", _from));
            }

            if(_followOnAction != null)
            {
                result.AddGood("There is a follow on action.");
            }

            return result;
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }
    }
}