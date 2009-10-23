namespace dropkick.tests
{
    using DeploymentModel;
    using Engine;
    using NUnit.Framework;
    

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
                return new DeploymentResult();
            }, () =>
            {
                executeRan = true;
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Trace
            };

            plan.Execute();

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
                return new DeploymentResult();
            }, () =>
            {
                executeRan = true;
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Verify
            };

            plan.Execute();

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
                return new DeploymentResult();
            }, () =>
            {
                executeRan = true;
                return new DeploymentResult();
            });


            var web = new DeploymentPart("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Execute
            };

            plan.Execute();

            Assert.IsTrue(traceRan);
            Assert.IsTrue(verifyRan);
            Assert.IsTrue(executeRan);
        }
    }
}