namespace dropkick.tests.Dsl
{
    using System;
    using Configuration.Dsl;
    using Engine;
    using Engine.DeploymentFinders;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
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
            DeploymentPlanRunner.Build(dep, _verifyArguments);
        }

        [Test]
        public void Verify_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            DeploymentPlanRunner.Build(dep, _verifyArguments);
        }

        [Test]
        public void Verify_Command()
        {
            Setup();
            var dep = new CommandTestDeploy();
            DeploymentPlanRunner.Build(dep, _verifyArguments);
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            DeploymentPlanRunner.Build(dep, _verifyArguments);
        }

        [Test]
        public void Verify_Iis()
        {
            var dep = new IisTestDeploy();
           DeploymentPlanRunner.Build(dep, _verifyArguments);
            
        }


        [Test]
        public void Verify_TestDeploy()
        {
            var dep = new TestDeployment();
            DeploymentPlanRunner.Build(dep, _verifyArguments);
            
        }
    }
}