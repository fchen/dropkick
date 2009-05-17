namespace dropkick.Dsl.Files
{
    using System;

    public class CopyTaskBuilder :
        CopyOptions
    {
        readonly string _from;
        string _to;
        readonly Part _part;
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