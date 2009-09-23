namespace dropkick.tests
{
    using dropkick.Execution;
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

            var detail = new DeploymentDetail(() =>
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
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Command = DropkickCommands.Trace
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

            var detail = new DeploymentDetail(() =>
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
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Command = DropkickCommands.Verify
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

            var detail = new DeploymentDetail(() =>
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
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new ExecutionArguments
            {
                Part = "WEB",
                Command = DropkickCommands.Execute
            };

            plan.Execute(args);

            Assert.IsTrue(traceRan);
            Assert.IsTrue(verifyRan);
            Assert.IsTrue(executeRan);
        }
    }
}