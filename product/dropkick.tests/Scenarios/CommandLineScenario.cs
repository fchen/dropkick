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
        Inspector _interpreter;

        [Test]
        public void Verify()
        {
            _interpreter = new Inspector();
            _verifyArguments = new ExecutionArguments()
            {
                DeploymentAssembly = GetType().Assembly.FullName,
                DeploymentFinder = new GenericDeploymentFinder<CommandTestDeploy>(),
                Environment = "TEST",
                Option = ExecutionOptions.Verify,
                Part = "WEB"
            };

            var dep = _verifyArguments.DeploymentFinder.Find("");
            dep.Inspect(_interpreter);

        }
    }

}