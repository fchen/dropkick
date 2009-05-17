namespace dropkick.tests.Dsl
{
    using dropkick.Dsl;
    using MbUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Adding_options
    {
        Deployment _deployment;

        [SetUp]
        public void Establish_Context()
        {
            _deployment = new SinglePartDeploy();
        }

        [Test]
        public void Run_Full()
        {
            _deployment.Run();
        }

        [Test]
        public void Run_one_part()
        {
            _deployment.Run();
        }
    }
}