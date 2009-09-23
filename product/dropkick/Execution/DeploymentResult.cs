namespace dropkick.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class DeploymentResult
    {
        readonly List<DeploymentItem> _items = new List<DeploymentItem>();
        StringBuilder sb = new StringBuilder();

        public IList<DeploymentItem> Results
        {
            get { return new ReadOnlyCollection<DeploymentItem>(_items); }
        }

        public void AddGood(string message)
        {
            _items.Add(new DeploymentItem(DeploymentItemStatus.Good, message));
        }

        public void AddGood(string messageFormat, params string[] args)
        {
            _items.Add(new DeploymentItem(DeploymentItemStatus.Good, string.Format(messageFormat, args)));
        }

        public void AddAlert(string message)
        {
            _items.Add(new DeploymentItem(DeploymentItemStatus.Alert, message));
        }

        public void AddAlert(string messageFormat, params string[] args)
        {
            _items.Add(new DeploymentItem(DeploymentItemStatus.Alert, string.Format(messageFormat, args)));
        }

        public void AddError(string message, Exception exception)
        {
            _items.Add(new DeploymentItem(DeploymentItemStatus.Error, message));
        }
    }
}