namespace dropkick.Configuration.Dsl.Iis
{
    using System;
    using DeploymentModel;
    using Tasks;

    public static class IisVersionSelector
    {
        public static IisSiteOptions SelectTheCorrectConfig(DeploymentServer server, string websiteName)
        {
            server.AddDetail(new NoteTask("IIS Version Detection Used").ToDetail());

            if (System.Environment.OSVersion.Version.Major == 1)
            {
                server.AddDetail(new NoteTask("IIS Version was automatically set to IIS6").ToDetail());
                return new Iis6TaskCfg(server, websiteName);
            }
            else if(System.Environment.OSVersion.Version.Major == 6)
            {
                server.AddDetail(new NoteTask("IIS Version was automatically set to IIS7").ToDetail());
                return new Iis7TaskCfg(server, websiteName);            
            }
            else
            {
                throw new Exception("Can't figure out which version of IIS to run");
            }
            
        }
    }
}