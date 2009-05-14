namespace dropkick.Dsl.Files
{
    using System;

    public static class Extension
    {
        public static CopyOptions CopyFrom(this Part part, string sourcePath)
        {
            return new CopyTaskBuilder(sourcePath, part);
        }
    }

    public interface CopyOptions
    {
        CopyOptions To(string targetPath);
        void And(Action<FileActions> copyAction);
    }

    public interface FileActions
    {
        FileAction WebConfig { get; }
        FileAction AppConfig { get; }
    }

    public interface FileAction
    {
        FileAction ReplaceIdentityTokensWithPrompt();
        FileAction EncryptIdentity();
    }

    public class CopyTaskBuilder :
        CopyOptions
    {
        string _from;
        string _to;
        Part _part;
        Action<FileActions> _followOn;
        CopyTask _task;

        public CopyTaskBuilder(string from, Part part)
        {
            _from = from;
            _part = part;
        }

        public CopyOptions To(string targetPath)
        {

            _to = targetPath;
            //add part?
            _task = new CopyTask(_from, _to);
            _part.AddTask(_task);
            return this;
        }

        public void And(Action<FileActions> copyAction)
        {
            //re-add part?
            _followOn = copyAction;
            _task.SetFollowOnAction(copyAction);
        }
    }
}