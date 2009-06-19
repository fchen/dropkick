namespace dropkick.Dsl.NetworkShare
{
    public interface FolderShareOptions
    {
        FolderShareOptions PointingTo(string path);
        void CreateIfNotExist();
    }
}