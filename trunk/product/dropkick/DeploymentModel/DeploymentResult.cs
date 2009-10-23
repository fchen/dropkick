namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DeploymentResult
    {
        readonly List<DeploymentItem> _items = new List<DeploymentItem>();

        public IList<DeploymentItem> Results
        {
            get { return new ReadOnlyCollection<DeploymentItem>(_items); }
        }

        public void AddGood(string message)
        {
            AddItem(DeploymentItemStatus.Good, message);
        }

        public void AddGood(string messageFormat, params string[] args)
        {
            AddItem(DeploymentItemStatus.Good, string.Format(messageFormat, args));
        }

        public void AddAlert(string message)
        {
            AddItem(DeploymentItemStatus.Alert, message);
        }

        public void AddAlert(string messageFormat, params string[] args)
        {
            AddItem(DeploymentItemStatus.Alert, string.Format(messageFormat, args));
        }

        public void AddError(string message, Exception exception)
        {
            AddItem(DeploymentItemStatus.Error, message);
        }

        void AddItem(DeploymentItemStatus status, string message)
        {
            _items.Add(new DeploymentItem(status, message));
        }

        public DeploymentResult MergedWith(DeploymentResult result)
        {
            foreach (var item in result.Results)
            {
                _items.Add(item);
            }

            return this;
        }
    }
}