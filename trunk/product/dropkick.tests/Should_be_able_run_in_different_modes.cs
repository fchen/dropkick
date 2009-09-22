namespace dropkick.tests
{
    using Engine;
    using Execution;
    using NUnit.Framework;
    using Verification;

    [TestFixture]
    public class Should_be_able_run_in_different_modes
    {

        [Test]
        public void Trace()
        {
            bool verifyRan = false;
            bool executeRan = false;
            bool traceRan = false;

            var detail = new ExecutionDetail(() =>
            {
                traceRan = true;
                return "trace";
            }, () =>
            {
                verifyRan = true;
                return new VerificationResult();
            }, () =>
            {
                executeRan = true;
                return new ExecutionResult();
            });


            var web = new ExecutionPart("WEB");
            web.AddDetail(detail);

            var plan = new ExecutionPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Option = ExecutionOptions.Trace
            };

            plan.Execute(args);

            Assert.IsTrue(traceRan);
            Assert.IsFalse(verifyRan);
            Assert.IsFalse(executeRan);
        }

        [Test]
        public void Verify()
        {
            bool verifyRan = false;
            bool executeRan = false;
            bool traceRan = false;

            var detail = new ExecutionDetail(() =>
            {
                traceRan = true;
                return "trace";
            }, () =>
            {
                verifyRan = true;
                return new VerificationResult();
            }, () =>
            {
                executeRan = true;
                return new ExecutionResult();
            });


            var web = new ExecutionPart("WEB");
            web.AddDetail(detail);

            var plan = new ExecutionPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Option = ExecutionOptions.Verify
            };

            plan.Execute(args);

            Assert.IsTrue(traceRan);
            Assert.IsTrue(verifyRan);
            Assert.IsFalse(executeRan);
        }

        [Test]
        public void Execute()
        {
            bool verifyRan = false;
            bool executeRan = false;
            bool traceRan = false;

            var detail = new ExecutionDetail(() =>
            {
                traceRan = true;
                return "trace";
            }, () =>
            {
                verifyRan = true;
                return new VerificationResult();
            }, () =>
            {
                executeRan = true;
                return new ExecutionResult();
            });


            var web = new ExecutionPart("WEB");
            web.AddDetail(detail);

            var plan = new ExecutionPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Option = ExecutionOptions.Execute
            };

            plan.Execute(args);

            Assert.IsTrue(traceRan);
            Assert.IsTrue(verifyRan);
            Assert.IsTrue(executeRan);
        }
    }
}