namespace dropkick.tests.Scenarios
{
    using Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class CommandLineScenario :
        Scenario
    {
        ExecutionArguments _verifyArguments;
        DeploymentInspector _interpreter;

        [Test]
        public void Verify()
        {
            _interpreter = new DeploymentInspector();
            _verifyArguments = new ExecutionArguments()
            {
                DeploymentAssembly = GetType().Assembly.FullName,
                DeploymentFinder = new GenericDeploymentFinder<CommandTestDeploy>(),
                Environment = "TEST",
                Option = DropkickCommands.Verify,
                Part = "WEB"
            };

            var dep = _verifyArguments.DeploymentFinder.Find("");
            dep.Inspect(_interpreter);

        }
    }

}