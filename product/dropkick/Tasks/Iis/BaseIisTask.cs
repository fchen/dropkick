namespace dropkick.Tasks.Iis
{
    using System;
    using System.IO;
    using Configuration.Dsl;
    using Execution;
    using Verification;

    public abstract class BaseIisTask :
        Task
    {
        public bool ShouldCreate { get; protected set; }
        public string WebsiteName { get; set; }
        public string VdirPath { get; set; }
        public DirectoryInfo PathOnServer { get; set; }
        public string ServerName { get; set; }
        public abstract int VersionNumber { get; }

        #region Task Members

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this);
        }

        public string Name
        {
            get
            {
                return "IIS{0}: Create vdir '{1}' in site '{2}' on server '{3}'".FormatWith(VersionNumber, VdirPath,
                                                                                            WebsiteName, ServerName);
            }
        }

        public abstract VerificationResult VerifyCanRun();
        public abstract ExecutionResult Execute();

        #endregion

        public void CheckServerName(VerificationResult result)
        {
            if (!Environment.MachineName.Equals(ServerName))
            {
                result.AddAlert("You are not on the right server [On: '{0}' - Target: '{1}'", Environment.MachineName,
                                ServerName);
            }
        }

        public void CheckForSiteAndVDirExistance(Func<bool> website, Func<bool> vdir, VerificationResult result)
        {
            if (website())
            {
                result.AddGood("Found Website '{0}'", WebsiteName);

                if (vdir())
                {
                    result.AddGood("Found VDir '{0}'", VdirPath);
                }
                else
                {
                    result.AddAlert("Couldn't find VDir '{0}'", VdirPath);

                    if (ShouldCreate)
                        result.AddAlert("The VDir '{0}' will be created", VdirPath);
                }
            }
            else
            {
                result.AddAlert("Couldn't find Website '{0}'", WebsiteName);

                if (ShouldCreate)
                    result.AddAlert("Website '{0}' and VDir '{1}' will be created", WebsiteName, VdirPath);
            }
        }
    }
}