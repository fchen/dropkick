namespace dropkick.tests.Execution
{
    using DeploymentModel;
    using NUnit.Framework;

    [TestFixture]
    public class Model_Specs
    {
        [Test]
        public void Simple_Execution_Smoke_Tests()
        {
            var ep = new DeploymentPlan();
            ep.Execute();

            var plan = new DeploymentPlan();
            plan.AddPart(new DeploymentPart("name"));
            plan.Execute();
        }

        [Test]
        public void Part()
        {
            var detail = new DeploymentDetail(() => "name_d", () => new DeploymentResult(), () => new DeploymentResult());
            var part = new DeploymentPart("name");
            part.AddDetail(detail);
            var plan = new DeploymentPlan();
            plan.AddPart(part);
            plan.Execute();

        }

    }
}