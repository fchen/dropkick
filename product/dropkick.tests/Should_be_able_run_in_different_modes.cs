namespace dropkick.tests
{
    using developwithpassion.bdd.mbunit;
    using Engine;
    using Execution;
    using MbUnit.Framework;
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

            traceRan.should_be_true();
            verifyRan.should_be_false();
            executeRan.should_be_false();
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

            traceRan.should_be_true();
            verifyRan.should_be_true();
            executeRan.should_be_false();
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

            traceRan.should_be_true();
            verifyRan.should_be_true();
            executeRan.should_be_true();
        }
    }
}