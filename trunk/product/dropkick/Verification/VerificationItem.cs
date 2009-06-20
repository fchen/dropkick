namespace dropkick.Verification
{
    public class VerificationItem
    {
        public VerificationItem(VerificationStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public VerificationStatus Status { get; private set; }
        public string Message { get; private set; }
    }
}