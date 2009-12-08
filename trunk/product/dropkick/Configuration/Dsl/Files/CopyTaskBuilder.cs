namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using Tasks.Files;

    public class CopyTaskBuilder :
    CopyOptions
    {
        private string _from;
        private CopyTask _task;
        private string _to;
        readonly Role _role;
        Action<FileActions> _followOn;

        public CopyTaskBuilder(Role role)
        {
            _role = role;
        }

        #region CopyOptions Members

        public CopyOptions From(string sourcePath)
        {
            _from = sourcePath;
            return this;
        }
        public CopyOptions To(string targetPath)
        {
            _to = targetPath;
            _task = new CopyTask(_from, _to);
            _role.AddTask(_task);
            return this;
        }

        public void With(Action<FileActions> copyAction)
        {
            _followOn = copyAction;
            copyAction(new SomeFileActions(_role));
        }

        #endregion
    }
}