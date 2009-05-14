namespace dropkick.Dsl.Iis
{
    public static class Extension
    {
        public static IisSiteOptions IisSite(this ServerOptions server, string websiteName)
        {
            return null;
        }
    }

    public interface IisSiteOptions
    {
        IisVirtualDirectoryOptions VirtualDirectory(string name);
        VerifyOptions Verify();
    }

    public interface IisVirtualDirectoryOptions
    {
        VerifyOptions Verify();
    }

    public interface VerifyOptions
    {
        void CreateIfItDoesntExist();
    }
}