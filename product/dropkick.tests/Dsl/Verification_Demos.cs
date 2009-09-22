namespace dropkick.tests.Dsl
{
    using System;
    using Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Verification_Demos
    {
        DeploymentInspector _interpreter;
        ExecutionArguments _verifyArguments;

        [SetUp]
        public void Setup()
        {
            _verifyArguments = new ExecutionArguments()
                               {
                                   DeploymentAssembly = GetType().Assembly.FullName,
                                   DeploymentFinder = new AssumesOnlyOneDeploymentFinder(),
                                   Environment = "TEST",
                                   Option = ExecutionOptions.Verify,
                                   Part = "WEB"
                               };

            _interpreter = new DeploymentInspector();
        }

        [TearDown]
        public void Teardown()
        {
            _interpreter = null;
        }

        [Test]
        public void Verify_MsSql_Test()
        {
            var dep = new MsSqlTestDeploy();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }

        [Test]
        public void Verify_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }

        [Test]
        public void Verify_Command()
        {
            Setup();
            var dep = new CommandTestDeploy();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }

        [Test]
        public void Verify_Iis()
        {
            var dep = new IisTestDeploy();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }


        [Test]
        public void Verify_TestDeploy()
        {
            var dep = new TestDeployment();
            dep.Inspect(_interpreter);
            var plan = _interpreter.GetPlan();
            plan.Execute(_verifyArguments);
        }
    }
}