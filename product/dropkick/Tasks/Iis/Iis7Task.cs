namespace dropkick.Tasks.Iis
{
    using System;
    using DeploymentModel;
    using Microsoft.Web.Administration;
    
    public class Iis7Task :
        BaseIisTask
    {
        public override int VersionNumber
        {
            get { return 7; }
        }

        public override DeploymentResult VerifyCanRun()
        {
            var result = new DeploymentResult();

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


        private void CheckVersionOfWindowsAndIis(DeploymentResult result)
        {
            int shouldBe6 = Environment.OSVersion.Version.Major;
            if (shouldBe6 != 6)
                result.AddAlert("This machine does not have IIS7 on it");
        }

        public bool DoesSiteExist()
        {
            var iisManager = new ServerManager();
            foreach (var site in iisManager.Sites)
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
            foreach (var site in iisManager.Sites)
            {
                if (!site.Name.Equals(base.WebsiteName)) continue;

                foreach (var app in site.Applications)
                {
                    if (app.Path.Equals("/" + VdirPath))
                    {
                        return true;
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