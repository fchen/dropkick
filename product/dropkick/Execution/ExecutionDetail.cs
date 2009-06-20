namespace dropkick.Execution
{
    using System;
    using Verification;

    public class ExecutionDetail
    {
        readonly Func<string> _name;
        readonly Func<VerificationResult> _verify;
        readonly Action _execute;

        public ExecutionDetail(Func<string> name, Func<VerificationResult> verify, Action execute)
        {
            _name = name;
            _verify = verify;
            _execute = execute;
        }

        public string Name
        {
            get { return _name(); }
        }

        public VerificationResult Verify()
        {
            return _verify();
        }

        public void Execute()
        {
            _execute();
        }
    }
}