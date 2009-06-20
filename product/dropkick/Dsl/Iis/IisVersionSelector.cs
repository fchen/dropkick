namespace dropkick.Dsl.Iis
{
    using System;

    public class IisVersionSelector
    {
        public static IisSiteOptions SelectTheCorrectConfig(ServerOptions server, string websiteName)
        {
            if (System.Environment.OSVersion.Version.Major == 1)
            {
                server.Part.AddTask(new NoteTask("IIS Version was automatically set to IIS6"));
                return new Iis6TaskCfg(server, websiteName);
            }
            else if(System.Environment.OSVersion.Version.Major == 6)
            {
                server.Part.AddTask(new NoteTask("IIS Version was automatically set to IIS7"));
                return new Iis7TaskCfg(server, websiteName);            
            }
            else
            {
                throw new Exception("Can't figure out which version of IIS to run");
            }
            
        }
    }
}