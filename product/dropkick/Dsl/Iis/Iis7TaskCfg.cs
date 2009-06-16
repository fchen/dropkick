namespace dropkick.Dsl.Iis
{
    using System;

    public class Iis7TaskCfg : 
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        public Iis7TaskCfg(ServerOptions server, string websiteName)
        {
            
        }

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            return this;
        }

        public void CreateIfItDoesntExist()
        {
            throw new NotImplementedException();
        }
    }
}