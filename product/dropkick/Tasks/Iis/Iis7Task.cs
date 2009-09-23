namespace dropkick.Tasks.Iis
{
    using System;
    using Execution;
    using Microsoft.Web.Administration;
    using Verification;

    public class Iis7Task :
        BaseIisTask
    {
        private bool _createIfItDoesntExist;

        //ctor

        public override int VersionNumber
        {
            get { return 7; }
        }

        public override VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();

            CheckVersionOfWindowsAndIis(result);

            CheckServerName(result);

            CheckForSiteAndVDirExistance(DoesSiteExist, DoesVirtualDirectoryExist, result);

            return result;
        }

        public override DeploymentResult Execute()
        {
            //ignore
            return new DeploymentResult();
        }


        private void CheckVersionOfWindowsAndIis(VerificationResult result)
        {
            int shouldBe6 = Environment.OSVersion.Version.Major;
            if (shouldBe6 != 6)
                result.AddAlert("This machine does not have IIS7 on it");
        }

        public bool DoesSiteExist()
        {
            var iisManager = new ServerManager();
            foreach (Site site in iisManager.Sites)
            {
                if (site.Name.Equals(base.WebsiteName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool DoesVirtualDirectoryExist()
        {
            var iisManager = new ServerManager();
            foreach (Site site in iisManager.Sites)
            {
                if (site.Name.Equals(base.WebsiteName))
                {
                    foreach (Application app in site.Applications)
                    {
                        if (app.Path.Equals("/" + VdirPath))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void CreateIfItDoesntExist()
        {
            ShouldCreate = true;
        }
    }
}