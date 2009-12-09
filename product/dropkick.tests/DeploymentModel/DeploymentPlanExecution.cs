namespace dropkick.tests.DeploymentModel
{
    using dropkick.DeploymentModel;
    using NUnit.Framework;

    public class DeploymentPlanExecution :
        WithSimpleModel
    {

        public DeploymentResult Result { get; private set; }

        public override void BecauseOf()
        {
            Result = Plan.Execute();
        }

        [Test]
        public void ShouldBeTwoResults()
        {
            Assert.AreEqual(2, Result.ResultCount);
        }
    }
}