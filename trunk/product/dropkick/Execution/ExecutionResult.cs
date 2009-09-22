using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace dropkick.Execution
{
    public class ExecutionResult
    {
        StringBuilder sb = new StringBuilder();
        readonly List<ExecutionItem> _items = new List<ExecutionItem>();

        public void AddGood(string message)
        {
            _items.Add(new ExecutionItem(ExecutionStatus.Good, message));
        }
        public void AddGood(string messageFormat, params string[] args)
        {
            _items.Add(new ExecutionItem(ExecutionStatus.Good, string.Format(messageFormat, args)));
        }

        public void AddAlert(string message)
        {
            _items.Add(new ExecutionItem(ExecutionStatus.Alert, message));
        }
        public void AddAlert(string messageFormat, params string[] args)
        {
            _items.Add(new ExecutionItem(ExecutionStatus.Alert, string.Format(messageFormat, args)));
        }

        public IList<ExecutionItem> Results
        {
            get { return new ReadOnlyCollection<ExecutionItem>(_items); }
        }

        public void AddError(string message, Exception exception)
        {
            _items.Add(new ExecutionItem(ExecutionStatus.Error, message));
        }
    }
}