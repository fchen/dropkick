namespace dropkick.Configuration.Dsl.Files
{
    using System;

    public interface CopyOptions
    {
        CopyOptions From(string sourcePath);
        void With(Action<FileActions> copyAction);
    }
}