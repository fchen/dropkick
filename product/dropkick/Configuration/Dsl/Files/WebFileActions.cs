namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using System.IO;
    using Tasks.CommandLine;

    public class WebFileActions :
        FileAction
    {
        readonly PartCfg _part;

        public WebFileActions(PartCfg part)
        {
            _part = part;
        }

        #region FileAction Members

        public FileAction ReplaceIdentityTokensWithPrompt()
        {
            //replace {{username}} and {{password}}?
            return this;
        }

        public FileAction EncryptIdentity()
        {
            var t = new CommandLineTask(@"aspnet_regiis");
            t.Args = @" -pe ""connectionStrings"" -app ""/MachineDPAPI"" -prov ""DataProtectionConfigurationProvider""";
            string winDir = Environment.GetEnvironmentVariable("WINDIR");
            t.ExecutableIsLocatedAt = Path.Combine(winDir, @"Microsoft.NET\Framework\v2.0.50727");

            _part.AddTask(t);

            return this;
        }

        #endregion
    }
}