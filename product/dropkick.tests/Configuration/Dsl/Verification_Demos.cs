// Copyright 2007-2008 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace dropkick.tests.Configuration.Dsl
{
    using dropkick.Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    [Category("Integration")]
    public class Verification_Demos
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _verifyArguments = new DeploymentArguments
                               {
                                   Deployment = GetType().Assembly.FullName,
                                   Environment = "TEST",
                                   Command = DeploymentCommands.Verify,
                                   Part = "WEB"
                               };
            _verifyArguments.ServerMappings.AddMap("WEB", "SrvTopeka02");
        }

        [TearDown]
        public void Teardown()
        {
        }

        #endregion

        DeploymentArguments _verifyArguments;

        [Test]
        public void Verify_Command()
        {
            Setup();
            var dep = new CommandTestDeploy();
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
        public void Verify_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        public void Verify_MsSql_Test()
        {
            var dep = new MsSqlTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }


        [Test]
        public void Verify_TestDeploy()
        {
            var dep = new TestDeployment();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            DeploymentPlanDispatcher.KickItOutThereAlready(dep, _verifyArguments);
        }
    }
}