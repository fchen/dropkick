namespace dropkick.Dsl.Iis
{
    using System;
    using System.DirectoryServices;
    using System.EnterpriseServices.Internal;
    using System.IO;
    using Verification;

    // thank you NAnt team for your help!! -dru
    public class Iis6Task :
        Task
    {
        bool _createIfItDoesntExist;
        string _iisPath = "IIS://localhost/W3svc/1/Root";

        public string WebsiteName { get; set; }
        public string VdirPath { get; set; }
        public DirectoryInfo PathOnServer { get; set; }
        public string ServerName { get; set; }

        #region Task Members

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return "IIS6: Create vdir '{0}' in site '{1}' on server '{2}'".FormatWith(VdirPath, WebsiteName, ServerName); }
        }

        public VerificationResult VerifyCanRun()
        {
            var result = new VerificationResult();
            
            CheckVersionOfWindowsAndIis(result);

            if (!Environment.MachineName.Equals(ServerName))
            {
                result.AddAlert("You are not on the right server [On: '{0}' - Target: '{1}'", Environment.MachineName,
                                ServerName);
            }

            if (!DirectoryEntryExists(WebsiteName , VdirPath))
            {
                if(_createIfItDoesntExist)
                    result.AddAlert("VDir not found, and will be created");
                else
                    result.AddAlert("VDir not found");
            }
            else
            {
                result.AddGood("Site Exists");
            }

            return result;
        }

        public void Execute()
        {
            DirectoryEntry vdir =
                GetOrMakeNode(WebsiteName, VdirPath, "IIsWebVirtualDir");
            vdir.RefreshCache();

            vdir.Properties["Path"].Value = PathOnServer.FullName;
            CreateApplication(vdir);
            SetIisProperties(vdir);

            vdir.CommitChanges();
            vdir.Close();
        }

        #endregion

        DirectoryEntry GetOrMakeNode(string basePath, string relPath, string schemaClassName)
        {
//            var vr = new IISVirtualRoot();
//            string error;
//            vr.Create("IIS://localhost/W3svc/1/Root", @"C:\bob", "dk_test", out error);

            if (DirectoryEntryExists(basePath, relPath))
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

        protected bool DirectoryEntryExists(string siteName, string vDirPath)
        {
            int siteNumber = ConvertSiteNameToSiteNumber(siteName);
            string path = BuildIisPath(siteNumber, vDirPath);
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

        void SetIisProperties(DirectoryEntry vdir)
        {
        }

        void CreateApplication(DirectoryEntry vdir)
        {
            vdir.Invoke("AppCreate2", 0);
        }

        public void CreateIfItDoesntExist()
        {
            _createIfItDoesntExist = true;
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