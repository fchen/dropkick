namespace dropkick.tests
{
    using DeploymentModel;
    using dropkick.DeploymentModel;
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


            var web = new DeploymentRole("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Trace
            };

            plan.Trace();

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


            var web = new DeploymentRole("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Verify
            };

            plan.Verify();

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
                var r = new DeploymentResult();
                r.AddGood("test:v");
                return r;
            }, () =>
            {
                executeRan = true;
                var r = new DeploymentResult();
                r.AddGood("test:e");
                return r;
            });


            var web = new DeploymentRole("WEB");
            web.AddDetail(detail);

            var plan = new DeploymentPlan();
            plan.AddPart(web);


            var args = new DeploymentArguments
            {
                Part = "WEB",
                Command = DeploymentCommands.Execute
            };

            plan.Execute();

            Assert.IsTrue(traceRan, "trace");
            Assert.IsTrue(verifyRan, "verify");
            Assert.IsTrue(executeRan, "execute");
        }
    }
}