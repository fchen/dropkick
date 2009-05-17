namespace dropkick.Dsl.Files
{
    using System;

    public interface CopyOptions
    {
        CopyOptions To(string targetPath);
        void And(Action<FileActions> copyAction);
    }
}