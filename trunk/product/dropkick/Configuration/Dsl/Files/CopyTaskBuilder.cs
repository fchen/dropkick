namespace dropkick.Dsl.Files
{
    using System;

    public class CopyTaskBuilder :
        CopyOptions
    {
        readonly string _from;
        string _to;
        readonly PartCfg _part;
        Action<FileActions> _followOn;
        CopyTask _task;

        public CopyTaskBuilder(string from, PartCfg part)
        {
            _from = from;
            _part = part;
        }

        public CopyOptions To(string targetPath)
        {
            _to = targetPath;
            _task = new CopyTask(_from, _to);
            _part.AddTask(_task);
            return this;
        }

        public void And(Action<FileActions> copyAction)
        {
            _followOn = copyAction;
            copyAction(new SomeFileActions(_part));
        }
    }
}