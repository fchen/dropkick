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
namespace dropkick.tests.DeploymentModel
{
    using dropkick.Configuration.Dsl;
    using dropkick.Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Should_be_able_to_execute_only_one_part
    {
        [Test]
        public void TryDb()
        {
            var dep = new TwoPartDeploy();
            var maps = new RoleToServerMap();
            maps.AddMap("WEB", "BOB");

            var ins = new DropkickDeploymentInspector();

            var plan = ins.GetPlan(dep, maps);

            Assert.AreEqual(1, plan.RoleCount);
        }

        [Test]
        public void TryWeb()
        {
            var dep = new TwoPartDeploy();
            var maps = new RoleToServerMap();
            maps.AddMap("WEB", "BOB");

            var ins = new DropkickDeploymentInspector();

            var plan = ins.GetPlan(dep, maps);

            Assert.AreEqual(1, plan.RoleCount);
        }
    }
}