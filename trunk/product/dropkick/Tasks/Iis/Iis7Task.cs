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
            var result = new DeploymentResult();

            if(DoesSiteExist())
            {
                result.AddGood("site exists");
                if(!DoesVirtualDirectoryExist())
                {
                    CreateVirtualDirectory();
                }
            }

            return result;
        }

        void CreateVirtualDirectory()
        {
            var iisManager = new ServerManager();
            Site siteToAdd = null;
            foreach (var site in iisManager.Sites)
            {
                if (site.Name.Equals(base.WebsiteName))
                {
                    siteToAdd = site;
                    break;
                }
            }

            Application appToAdd = null;
            foreach (var app in siteToAdd.Applications)
            {
                if (app.Path.Equals("/" + VdirPath))
                {
                    appToAdd = app;
                }
            }

            if(appToAdd == null)
            {
                //create it
                siteToAdd.Applications.Add("/" +VdirPath, PathOnServer.FullName);
            }
            iisManager.CommitChanges();
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