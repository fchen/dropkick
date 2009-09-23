namespace dropkick.Execution
{
    public class DeploymentItem
    {
        public DeploymentItemStatus Status { get; set; }
        public string Message { get; set; }

        public DeploymentItem(DeploymentItemStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}