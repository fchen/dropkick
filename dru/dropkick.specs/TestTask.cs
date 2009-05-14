namespace dropkick.specs
{
    using System;
    using Dsl;
    using Engine;

    public class TestTask :
        Task
    {
        public bool WasRun { get; set; }

        public TestTask()
        {
            WasRun = false;
        }

        public void Execute()
        {
            WasRun = true;
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