namespace dropkick.tests.Dsl
{
    using dropkick.Dsl;
    using dropkick.Dsl.Visitor;
    using MbUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Adding_options
    {
        Deployment _deployment;

        [SetUp]
        public void Establish_Context()
        {
            _deployment = new TwoPartDeploy();
        }

        [Test]
        public void Run_Full()
        {
            var visitor = new ExecutionInspector();
            _deployment.Inspect(visitor);
        }

        [Test]
        public void Run_one_part()
        {
            var visitor = new ExecutionInspector();
            _deployment.Inspect(visitor);
        }
    }
}