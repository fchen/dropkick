namespace dropkick.tests.Configuration.Dsl
{
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Deployments_should_have_roles
    {
        [Test]
        public void Should_have_one_role()
        {
            var dep = new SinglePartDeploy();
            var pc = new RoleCounter();
            dep.InspectWith(pc);
            Assert.AreEqual(1,pc.Count);
        }

        [Test]
        public void Should_have_two_roles()
        {
            var dep = new TwoPartDeploy();
            var pc = new RoleCounter();
            dep.InspectWith(pc);
            Assert.AreEqual(2, pc.Count);
        }
    }
}