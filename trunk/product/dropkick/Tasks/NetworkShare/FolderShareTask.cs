namespace dropkick.Tasks.NetworkShare
{
    using System;
    using System.IO;
    using System.Management;
    using Configuration.Dsl;
    using Execution;
    using Verification;

    public class FolderShareTask :
        Task
    {
        private bool _createIfNotExist;

        public string Server { get; set; }
        public string Description { get; set; }
        public string ShareName { get; set; }
        public string PointingTo { get; set; }

        #region Task Members

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public VerificationResult VerifyCanRun()
        {
            //verify admin
            var result = new VerificationResult();

            if (!Directory.Exists(PointingTo))
                result.AddAlert("'{0}' doesn't exist", PointingTo);
            else
                result.AddGood("'{0}' exists", PointingTo);


            return result;
        }


        public ExecutionResult Execute()
        {
            var result = new ExecutionResult();
            var managementClass = new ManagementClass("Win32_Share");

            ManagementBaseObject args = managementClass.GetMethodParameters("Create");
            args["Description"] = Description;
            args["Name"] = ShareName;
            args["Path"] = PointingTo;
            args["Type"] = 0x0; // Disk Drive

            ManagementBaseObject outParams = managementClass.InvokeMethod("Create", args, null);

            // Check to see if the method invocation was successful
            if (outParams != null && (uint) (outParams.Properties["ReturnValue"].Value) != 0)
            {
                throw new Exception("Unable to share directory '{0}' as '{2}' on '{1}'.".FormatWith(PointingTo, Server,
                                                                                                    ShareName));
            }

            result.AddGood("Created share");

            return result;
        }

        public string Name
        {
            get { return "Share Folder '{0}' as '{1}' on '{2}'".FormatWith(PointingTo, ShareName, Server); }
        }

        #endregion

        public void CreateIfNotExist()
        {
            _createIfNotExist = true;
        }
    }
}