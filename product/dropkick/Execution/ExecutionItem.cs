namespace dropkick.Execution
{
    public class ExecutionItem
    {
        public ExecutionStatus Status { get; set; }
        public string Message { get; set; }

        public ExecutionItem(ExecutionStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}