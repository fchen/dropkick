namespace dropkick.Execution
{
    using System.Diagnostics;

    [DebuggerDisplay("[{Status}:{Message}")]
    public class DeploymentItem
    {
        public DeploymentItem(DeploymentItemStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public DeploymentItemStatus Status { get; set; }
        public string Message { get; set; }
    }
}