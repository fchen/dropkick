namespace dropkick.Execution
{
    using System;
    using Verification;

    public class DeploymentDetail
    {
        readonly Func<string> _name;
        readonly Func<VerificationResult> _verify;
        readonly Func<DeploymentResult> _execute;

        public DeploymentDetail(Func<string> name, Func<VerificationResult> verify, Func<DeploymentResult> execute)
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

        public DeploymentResult Execute()
        {
            return _execute();
        }
    }
}