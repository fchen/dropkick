namespace dropkick.tests.DeploymentModel
{
    using dropkick.DeploymentModel;
    using NUnit.Framework;

    public class DeploymentPlanVerification :
        WithSimpleModel
    {

        public DeploymentResult Result { get; private set; }

        public override void BecauseOf()
        {
            Result = Plan.Verify();
        }

        [Test]
        public void ShouldOnlyBeOneResult()
        {
            Assert.AreEqual(1, Result.ResultCount);
        }
    }
}