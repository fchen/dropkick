namespace dropkick.Dsl.NetworkShare
{
    using System;
    using System.Management;
    using Verification;

    public class FolderShareTask :
        Task
    {
        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public VerificationResult VerifyCanRun()
        {
            throw new NotImplementedException();
        }

        //http://www.codeproject.com/KB/system/Share-Folder-c_.aspx
        public void Execute()
        {
            var managementClass = new ManagementClass("Win32_Share");

            var inParams = managementClass.GetMethodParameters("Create");

            // Set the input parameters


            inParams["Description"] = Description;

            inParams["Name"] = ShareName;

            inParams["Path"] = PointingTo;

            inParams["Type"] = 0x0; // Disk Drive
            
            var outParams = managementClass.InvokeMethod("Create", inParams, null);

            // Check to see if the method invocation was successful


            if (outParams != null && (uint)(outParams.Properties["ReturnValue"].Value) != 0)
            {
                throw new Exception("Unable to share directory.");
            }
        }

        bool _createIfNotExist;

        public string Server { get; set; }
        public string Description { get; set; }
        public string Name
        {
            get { return "NETWORK SHARE: bob"; }
        }

        public string ShareName { get; set; }

        public string PointingTo { get; set; }

        public void CreateIfNotExist()
        {
            _createIfNotExist = true;
        }
    }
}