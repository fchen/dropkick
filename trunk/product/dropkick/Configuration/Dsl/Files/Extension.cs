namespace dropkick.Dsl.Files
{
    public static class Extension
    {
        public static CopyOptions CopyFrom(this Part part, string sourcePath)
        {
            //because I don't use the server name I had to do this upcast
            return new CopyTaskBuilder(sourcePath, (PartCfg)part);
        }
    }
}