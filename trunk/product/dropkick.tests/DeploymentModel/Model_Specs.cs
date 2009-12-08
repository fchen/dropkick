namespace dropkick.tests.DeploymentModel
{
    using dropkick.DeploymentModel;
    using NUnit.Framework;

    [TestFixture]
    public class Model_Specs
    {
        DeploymentPlan _plan;
        DeploymentResult _result;

        [SetUp]
        public void SetUp()
        {
            var detail = new DeploymentDetail(() => "name_d", () => new DeploymentResult()
                                                                    {
                                                                        new DeploymentItem(DeploymentItemStatus.Good, "verify")
                                                                    }, () => new DeploymentResult()
                                                                             {
                                                                                 new DeploymentItem(DeploymentItemStatus.Good, "execute")
                                                                             });
            var role = new DeploymentRole("name");
            role.AddServer(new DeploymentServer("bob"));
            role.ForEachServer(s=>s.AddDetail(detail));
            _plan = new DeploymentPlan();
            _plan.AddPart(role);

            BecauseOf();
        }

        public void BecauseOf()
        {
            _result  = _plan.Execute();
        }

        [Test]
        public void Part()
        {
            Assert.AreEqual(2, _result.Results.Count);
       
        }

    }
}