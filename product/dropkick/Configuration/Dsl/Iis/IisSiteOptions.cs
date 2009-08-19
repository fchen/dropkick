namespace dropkick.Dsl.Iis
{
    public interface IisSiteOptions
    {
        IisVirtualDirectoryOptions VirtualDirectory(string name);
    }
}