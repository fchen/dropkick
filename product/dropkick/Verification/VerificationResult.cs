namespace dropkick.Verification
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
        public void AddGood(string messageFormat, params string[] args)
        {
            _items.Add(new VerificationItem(VerificationStatus.Good,string.Format(messageFormat, args)));
        }

        public void AddAlert(string message)
        {
            _items.Add(new VerificationItem(VerificationStatus.Alert, message));
        }
        public void AddAlert(string messageFormat, params string[] args)
        {
            _items.Add(new VerificationItem(VerificationStatus.Alert, string.Format(messageFormat, args)));
        }

        public IList<VerificationItem> Results
        {
            get { return new ReadOnlyCollection<VerificationItem>(_items); }
        }
    }
}