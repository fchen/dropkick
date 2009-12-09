namespace dropkick.tests.DeploymentModel
{
    using dropkick.DeploymentModel;
    using NUnit.Framework;

    [TestFixture]
    public abstract class WithSimpleModel
    {
        public DeploymentPlan Plan { get; private set;}

        [TestFixtureSetUp]
        public void SetUp()
        {
            var detail = new DeploymentDetail(() => "test detail", () => new DeploymentResult()
                                                                    {
                                                                        new DeploymentItem(DeploymentItemStatus.Good, "verify")
                                                                    }, () => new DeploymentResult()
                                                                             {
                                                                                 new DeploymentItem(DeploymentItemStatus.Good, "execute")
                                                                             });
            var role = new DeploymentRole("test role");
            role.AddServer(new DeploymentServer("srvtest"));
            role.ForEachServer(s=>s.AddDetail(detail));
            Plan = new DeploymentPlan();
            Plan.AddRole(role);

            BecauseOf();
        }

        public abstract void BecauseOf();
    }
}