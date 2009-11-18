namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using Tasks.Files;

    public class CopyTaskBuilder :
        CopyOptions
    {
        private readonly string _from;
        private readonly RoleCfg _role;
        private Action<FileActions> _followOn;
        private CopyTask _task;
        private string _to;

        public CopyTaskBuilder(string from, RoleCfg role)
        {
            _from = from;
            _role = role;
        }

        #region CopyOptions Members

        public CopyOptions To(string targetPath)
        {
            _to = targetPath;
            _task = new CopyTask(_from, _to);
            _role.AddTask(_task);
            return this;
        }

        public void And(Action<FileActions> copyAction)
        {
            _followOn = copyAction;
            copyAction(new SomeFileActions(_role));
        }

        #endregion
    }
}