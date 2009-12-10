namespace dropkick.tests.DeploymentModel
{
    using dropkick.DeploymentModel;
    using NUnit.Framework;

    [TestFixture]
    public abstract class WithFailingModel
    {
        public DeploymentPlan Plan { get; private set; }

        [TestFixtureSetUp]
        public void SetUp()
        {
            var successDetail = new DeploymentDetail(() => "success detail", () => new DeploymentResult()
                                                                                       {
                                                                                           new DeploymentItem(DeploymentItemStatus.Good, "verify")
                                                                                       }, () => new DeploymentResult()
                                                                                                    {
                                                                                                        new DeploymentItem(DeploymentItemStatus.Good, "execute")
                                                                                                    });

            var failDetail = new DeploymentDetail(() => "fail detail", () => new DeploymentResult()
                                                                                 {
                                                                                     new DeploymentItem(DeploymentItemStatus.Error, "fail verify")
                                                                                 }, () => new DeploymentResult()
                                                                                              {
                                                                                                  new DeploymentItem(DeploymentItemStatus.Good, "execute")
                                                                                              });


            var webRole = new DeploymentRole("Web");
            webRole.AddServer(new DeploymentServer("SrvWeb1"));
            webRole.AddServer(new DeploymentServer("SrvWeb2"));
            webRole.ForEachServer(s => s.AddDetail(successDetail));
            var dbRole = new DeploymentRole("Db");
            dbRole.AddServer("SrvDb");
            dbRole.ForEachServer(s => s.AddDetail(failDetail));
            Plan = new DeploymentPlan();
            Plan.AddRole(webRole);
            Plan.AddRole(dbRole);

            BecauseOf();
        }

        public abstract void BecauseOf();
    }
}