namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using System.IO;
    using Tasks.CommandLine;

    public class WebFileActions :
        FileAction
    {
        readonly Server _server;

        public WebFileActions(Server server)
        {
            _server = server;
        }

        #region FileAction Members

        public FileAction ReplaceIdentityTokensWithPrompt()
        {
            //replace {{username}} and {{password}}?
            return this;
        }

        public FileAction EncryptIdentity()
        {
            var t = new LocalCommandLineTask(@"aspnet_regiis");
            t.Args = @" -pe ""connectionStrings"" -app ""/MachineDPAPI"" -prov ""DataProtectionConfigurationProvider""";
            string winDir = Environment.GetEnvironmentVariable("WINDIR");
            t.ExecutableIsLocatedAt = Path.Combine(winDir, @"Microsoft.NET\Framework\v2.0.50727");

            _server.Role.AddTask(t);

            return this;
        }

        #endregion
    }
}