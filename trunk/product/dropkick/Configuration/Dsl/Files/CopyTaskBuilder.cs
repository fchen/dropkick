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
        readonly Server _server;
        Action<FileActions> _followOn;

        public CopyTaskBuilder(Server server)
        {
            _server = server;
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
            _server.Role.AddTask(_task);
            return this;
        }

        public void With(Action<FileActions> copyAction)
        {
            _followOn = copyAction;
            copyAction(new SomeFileActions(_server));
        }

        #endregion
    }
}