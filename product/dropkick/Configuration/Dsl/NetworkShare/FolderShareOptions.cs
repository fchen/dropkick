namespace dropkick.Configuration.Dsl.NetworkShare
{
    public interface FolderShareOptions
    {
        FolderShareOptions PointingTo(string path);
        void CreateIfNotExist();
    }
}