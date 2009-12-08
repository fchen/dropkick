namespace dropkick.Configuration.Dsl.Files
{
    using System;
    using DeploymentModel;
    using Tasks.Files;

    public class CopyTaskBuilder :
    CopyOptions
    {
        private string _from;
        private CopyTask _task;
        private string _to;
        readonly DeploymentServer _server;
        Action<FileActions> _followOn;

        public CopyTaskBuilder(DeploymentServer server)
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
            _server.AddDetail(_task.ToDetail());
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