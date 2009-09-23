using dropkick.Execution;

namespace dropkick.tests.TestObjects
{
    using System;
    using Configuration.Dsl;
    using Verification;

    public class TestTask :
        Task
    {
        public bool WasRun { get; set; }

        public TestTask()
        {
            WasRun = false;
        }

        public DeploymentResult Execute()
        {
            WasRun = true;
            return new DeploymentResult();
        }

        public void Inspect(DeploymentInspector inspector)
        {
            Console.WriteLine("task inspection");
            inspector.Inspect(this);
        }

        public string Name
        {
            get { return "test"; }
        }

        public VerificationResult VerifyCanRun()
        {
            return new VerificationResult();
        }
    }
}