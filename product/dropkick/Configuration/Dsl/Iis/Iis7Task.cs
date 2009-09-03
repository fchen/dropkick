namespace dropkick.Configuration.Dsl.Iis
{
    using Microsoft.Web.Administration;
    using Verification;

    public class Iis7Task :
        BaseIisTask
    {
        bool _createIfItDoesntExist;

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

        public override void Execute()
        {
            //ignore
        }

        


        private void CheckVersionOfWindowsAndIis(VerificationResult result)
        {
            var shouldBe6 = System.Environment.OSVersion.Version.Major;
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
                if (site.Name.Equals(base.WebsiteName))
                {

                    foreach (var app in site.Applications)
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