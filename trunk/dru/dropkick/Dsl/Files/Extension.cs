namespace dropkick.Dsl.Files
{
    public static class Extension
    {
        public static CopyOptions CopyFrom(this Part part, string sourcePath)
        {
            return new CopyTaskBuilder(sourcePath, part);
        }
    }
}