namespace dropkick.Dsl.Files
{
    using System;
    using System.IO;
    using Environment=dropkick.Dsl.Environment;

    public class WebFileActions :
        FileAction
    {
        string fileName = "web.config";
        PartCfg _part;

        public WebFileActions(PartCfg part)
        {
            _part = part;
        }

        public FileAction ReplaceIdentityTokensWithPrompt()
        {
            //replace {{username}} and {{password}}?
            return this;
        }

        public FileAction EncryptIdentity()
        {
            var t = new CommandLine.CommandLineTask(@"aspnet_regiis");
            t.Args = @" -pe ""connectionStrings"" -app ""/MachineDPAPI"" -prov ""DataProtectionConfigurationProvider""";
            var winDir = System.Environment.GetEnvironmentVariable("WINDIR");
            t.ExecutableIsLocatedAt = Path.Combine(winDir, @"Microsoft.NET\Framework\v2.0.50727");
            
            _part.AddTask(t);

            return this;
        }
    }
}