namespace dropkick.tests.Execution
{
    using DeploymentModel;
    using NUnit.Framework;

    [TestFixture]
    public class ExecutionPlan_Specs
    {
        [Test]
        public void Empty_Plan()
        {
            var ep = new DeploymentPlan();
            ep.Execute();
        }

        [Test]
        public void One_Action()
        {
            var ep = new DeploymentPlan();
            ep.AddPart(new DeploymentPart("name"));
            ep.Execute();
        }
    }
}