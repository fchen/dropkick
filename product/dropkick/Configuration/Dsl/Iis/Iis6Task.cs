using dropkick.Execution;

namespace dropkick.Configuration.Dsl.Iis
{
    using System;
    using System.DirectoryServices;
    using Verification;

    public class Iis6Task :
        BaseIisTask
    {
        bool _createIfItDoesntExist;
        
        //ctor

        public override int VersionNumber
        {
            get { return 6; }
        }

        public override VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            
            CheckVersionOfWindowsAndIis(result);

            CheckServerName(result);

            CheckForSiteAndVDirExistance(DoesSiteExist, DoesVirtualDirectoryExist, result);

            return result;
        }

        public override ExecutionResult Execute()
        {
            DirectoryEntry vdir =
                GetOrMakeNode(WebsiteName, VdirPath, "IIsWebVirtualDir");
            vdir.RefreshCache();

            vdir.Properties["Path"].Value = PathOnServer.FullName;
            CreateApplication(vdir);
            SetIisProperties(vdir);

            vdir.CommitChanges();
            vdir.Close();

            return new ExecutionResult();
        }

        
        public bool DoesSiteExist()
        {
            return ConvertSiteNameToSiteNumber(WebsiteName) > 0;
        }



        protected bool DoesVirtualDirectoryExist()
        {
            int siteNumber = ConvertSiteNameToSiteNumber(WebsiteName);
            string path = BuildIisPath(siteNumber, VdirPath);
            var entry = new DirectoryEntry(path);

            try
            {
                //trigger the *private* entry.Bind() method
                object adsobject = entry.NativeObject;
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                entry.Dispose();
            }
        }

        DirectoryEntry GetOrMakeNode(string basePath, string relPath, string schemaClassName)
        {
            //            var vr = new IISVirtualRoot();
            //            string error;
            //            vr.Create("IIS://localhost/W3svc/1/Root", @"C:\bob", "dk_test", out error);

            if (DoesVirtualDirectoryExist())
            {
                return new DirectoryEntry(basePath + relPath);
            }
            var parent = new DirectoryEntry(basePath);
            parent.RefreshCache();
            DirectoryEntry child = parent.Children.Add(relPath.Trim('/'), schemaClassName);
            child.CommitChanges();
            parent.CommitChanges();
            parent.Close();
            return child;
        }
        void SetIisProperties(DirectoryEntry vdir)
        {
        }

        void CreateApplication(DirectoryEntry vdir)
        {
            vdir.Invoke("AppCreate2", 0);
        }

        public void CreateIfItDoesntExist()
        {
            ShouldCreate = true;
        }

        void CheckVersionOfWindowsAndIis(VerificationResult result)
        {
            int shouldBe5 = Environment.OSVersion.Version.Major;
            if (shouldBe5 != 5)
                result.AddAlert("This machine does not have IIS6 on it");
        }

        string BuildIisPath(int siteNumber, string vDirPath)
        {
            return string.Format("IIS://localhost/w3svc/{0}/Root/{1}", siteNumber, vDirPath);
        }

        int ConvertSiteNameToSiteNumber(string name)
        {
            var e = new DirectoryEntry("IIS://localhost/W3SVC");
            e.RefreshCache();
            foreach (DirectoryEntry entry in e.Children)
            {
                if (entry.SchemaClassName != "IIsWebServer")
                {
                    entry.Close();
                    continue;
                }

                var x = entry.Name;
                entry.Close();
                return int.Parse(x);
            }
            
            throw new Exception("could find your website");
        }
    }
}