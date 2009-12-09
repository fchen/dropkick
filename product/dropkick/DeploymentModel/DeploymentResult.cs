namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class DeploymentResult :
        IEnumerable<DeploymentItem>
    {
        readonly List<DeploymentItem> _items = new List<DeploymentItem>();

        public IList<DeploymentItem> Results
        {
            get { return new ReadOnlyCollection<DeploymentItem>(_items); }
        }

        public int ResultCount
        {
            get
            {
                return _items.Count;
            }
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

        public void AddError(string message)
        {
            AddItem(DeploymentItemStatus.Error, message);
        }
        public void AddError(string message, Exception exception)
        {
            AddItem(DeploymentItemStatus.Error, message);
        }

        void AddItem(DeploymentItemStatus status, string message)
        {
            _items.Add(new DeploymentItem(status, message));
        }
        public void Add(DeploymentItem item)
        {
            _items.Add(item);
        }
        public DeploymentResult MergedWith(DeploymentResult result)
        {
            foreach (var item in result.Results)
            {
                _items.Add(item);
            }

            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<DeploymentItem> GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
            yield break;
        }
    }
}