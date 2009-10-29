namespace dropkick.tests
{
    using DeploymentModel;
    using dropkick.Configuration.Dsl;
    using dropkick.DeploymentModel;
    using Engine;
    using NUnit.Framework;
    using TestObjects;


    [TestFixture]
    public class Should_be_able_to_execute_only_one_part
    {
        [Test]
        public void TryWeb()
        {
            var dep = new TwoPartDeploy();

            var ins = new DropkickDeploymentInspector();

            var plan = ins.GetPlan(dep, p =>
            {
                return p.Name == "Web";
            });

            Assert.AreEqual(1, plan.PartCount);   
        }

        [Test]
        public void TryDb()
        {
            var dep = new TwoPartDeploy();

            var ins = new DropkickDeploymentInspector();

            var plan = ins.GetPlan(dep, p =>
            {
                return p.Name == "Db";
            });
            
            Assert.AreEqual(1, plan.PartCount);
        }
    }
}