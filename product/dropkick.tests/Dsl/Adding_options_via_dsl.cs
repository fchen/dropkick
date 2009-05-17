namespace dropkick.tests.Dsl
{
    using dropkick.Dsl.Visitor;
    using MbUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Adding_options_via_dsl
    {
        [Test]
        public void Should_have_one_part()
        {
            var dep = new SinglePartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.AreEqual(1,pc.Count);
        }

        [Test]
        public void Should_have_two_parts()
        {
            var dep = new TwoPartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.AreEqual(2, pc.Count);
        }

        [Test]
        public void Trace_Test()
        {
            var dep = new SinglePartDeploy();
            var pc = new TraceVisitor();
            dep.Inspect(pc);
        }

        [Test]
        public void Verify_Test()
        {
            var dep = new SinglePartDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }
    }
}