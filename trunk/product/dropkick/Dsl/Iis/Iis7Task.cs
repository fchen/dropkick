namespace dropkick.Dsl.Iis
{
    using System;
    using System.IO;
    using Verification;

    public class Iis7Task :
        Task
    {
        public string WebsiteName { get; set; }
        public string VdirPath { get; set; }
        public DirectoryInfo PathOnServer { get; set; }
        public string ServerName { get; set; }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return "IIS7: Create vdir '{0}' in site '{1}' on server '{2}'".FormatWith(VdirPath, WebsiteName, ServerName); }
        }

        public VerificationResult VerifyCanRun()
        {
            return new VerificationResult();
        }

        public void Execute()
        {
            //ignore
        }

        bool _createIfItDoesntExist;

        private void CheckVersionOfWindowsAndIis(VerificationResult result)
        {
            var shouldBe6 = System.Environment.OSVersion.Version.Major;
            if (shouldBe6 != 6)
                result.AddAlert("This machine does not have IIS7 on it");
        }

        public void CreateIfItDoesntExist()
        {
            _createIfItDoesntExist = true;
        }
    }
}