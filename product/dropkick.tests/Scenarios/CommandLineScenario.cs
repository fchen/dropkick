namespace dropkick.tests.Scenarios
{
    using dropkick.Configuration.Dsl;
    using Engine.DeploymentFinders;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class CommandLineScenario :
        Scenario
    {
        DropkickDeploymentInspector _interpreter;

        [Test]
        public void Verify()
        {
            _interpreter = new DropkickDeploymentInspector();

            var dep = new GenericDeploymentFinder<CommandTestDeploy>().Find("");
            dep.Inspect(_interpreter);
        }
    }
}