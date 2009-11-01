namespace dropkick.Tasks.Files
{
    using System;
    using System.IO;
    using Configuration.Dsl;
    using DeploymentModel;
    using Exceptions;


    public class CopyTask :
        Task
    {
        private string _from;
        private string _to;

        public CopyTask(string @from, string to)
        {
            _from = from;
            _to = to;
        }

        #region Task Members

        public string Name
        {
            get { return string.Format("Copy '{0}' to '{1}'", _from, _to); }
        }

        public DeploymentResult Execute()
        {
            var result = new DeploymentResult();

            ValidatePath(result, _to);
            ValidatePath(result, _from);

            _from = Path.GetFullPath(_from);
            _to = Path.GetFullPath(_to);

            //todo: verify that from exists
            if (!Directory.Exists(_to))
            {
                Directory.CreateDirectory(_to);
            }

            if (Directory.Exists(_from))
            {
                foreach (string file in Directory.GetFiles(_from))
                {
                    //need to support recursion
                    string fileName = Path.GetFileName(file);
                    File.Copy(file, Path.Combine(_to, fileName));
                    //log file was copied / event?
                }

                //what do you want to do if the directory DOESN'T exist?
            }

            result.AddGood("Copied stuff");

            return result;
        }

        public DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();

            ValidatePath(result, _to);
            ValidatePath(result, _from);

            _from = Path.GetFullPath(_from);
            _to = Path.GetFullPath(_to);

            //check can write from _to
            if (!Directory.Exists(_to))
                result.AddAlert(string.Format("'{0}' doesn't exist and will be created", _to));

            if (Directory.Exists(_from))
            {
                result.AddGood(string.Format("'{0}' exists", _from));
                //check can read from _from
                string[] readFiles = Directory.GetFiles(_from);
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
                        result.AddAlert("CopyTask: Can't read file '{0}'");
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

            return result;
        }

        void ValidatePath(DeploymentResult result, string path)
        {
            try
            {
                Path.GetFullPath(_to);
            }
            catch (Exception ex)
            {
                throw new DeploymentException("'{0}' is not an acceptable path".FormatWith(_to));
            }
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        #endregion
    }
}