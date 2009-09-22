namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using Tasks.Files;

    public class CopyTaskBuilder :
        CopyOptions
    {
        private readonly string _from;
        private readonly PartCfg _part;
        private Action<FileActions> _followOn;
        private CopyTask _task;
        private string _to;

        public CopyTaskBuilder(string from, PartCfg part)
        {
            _from = from;
            _part = part;
        }

        #region CopyOptions Members

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

        #endregion
    }
}