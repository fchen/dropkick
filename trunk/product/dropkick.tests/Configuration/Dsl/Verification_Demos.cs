namespace dropkick.tests.Configuration.Dsl
{
    using Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    [Category("Integration")]
    public class Verification_Demos
    {
        DeploymentArguments _verifyArguments;

        [SetUp]
        public void Setup()
        {
            _verifyArguments = new DeploymentArguments()
                               {
                                   Deployment = GetType().Assembly.FullName,
                                   Environment = "TEST",
                                   Command = DeploymentCommands.Verify,
                                   Part = "WEB"
                               };
        }

        [TearDown]
        public void Teardown()
        {
        }

        [Test]
        public void Verify_MsSql_Test()
        {
            var dep = new MsSqlTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        public void Verify_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        public void Verify_Command()
        {
            Setup();
            var dep = new CommandTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        [Category("Integration")]
        public void Verify_Iis()
        {
            var dep = new IisTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
            
        }


        [Test]
        public void Verify_TestDeploy()
        {
            var dep = new TestDeployment();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
            
        }
    }
}