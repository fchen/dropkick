namespace dropkick.Configuration.Dsl.Iis
{
    public interface IisSiteOptions
    {
        IisVirtualDirectoryOptions VirtualDirectory(string name);
    }
}