namespace dropkick.tests.Dsl
{
    using dropkick.Dsl.Visitor;
    using Engine;
    using MbUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Deployments_should_have_parts
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
    }
}