namespace dropkick.tests.Execution
{
    using DeploymentModel;
    using Engine;
    using NUnit.Framework;

    [TestFixture]
    public class ExecutionPlan_Specs
    {
        [Test]
        public void Empty_Plan()
        {
            var ep = new DeploymentPlan();
            var args = new DeploymentArguments();
            ep.Execute(args);
        }

        [Test]
        public void One_Action()
        {
            var ep = new DeploymentPlan();
            var args = new DeploymentArguments();
            ep.AddPart(new DeploymentPart("name"));
            ep.Execute(args);
        }
    }
}