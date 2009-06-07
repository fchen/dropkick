namespace dropkick.Engine
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;

    public class VerificationResult
    {
        StringBuilder sb = new StringBuilder();
        readonly List<VerificationItem> _items = new List<VerificationItem>();

        public void AddGood(string message)
        {
            _items.Add(new VerificationItem(VerificationStatus.Good, message));
        }

        public void AddAlert(string message)
        {
            _items.Add(new VerificationItem(VerificationStatus.Alert, message));
        }

        public void AddError(string message)
        {
            _items.Add(new VerificationItem(VerificationStatus.Error, message));
        }
        public void AddError(string messageFormat, params string[] value)
        {
            AddError(string.Format(messageFormat, value));
        }
        public IList<VerificationItem> Results
        {
            get { return new ReadOnlyCollection<VerificationItem>(_items); }
        }
    }
}