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
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }

        [Test]
        public void Verify_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }

        [Test]
        public void Verify_Command()
        {
            Setup();
            var dep = new CommandTestDeploy();
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }

        [Test]
        public void Verify_Iis()
        {
            var dep = new IisTestDeploy();
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }


        [Test]
        public void Verify_TestDeploy()
        {
            var dep = new TestDeployment();
            var plan = DeploymentPlanBuilder.Build(dep, _verifyArguments);
            plan.Execute();
        }
    }
}