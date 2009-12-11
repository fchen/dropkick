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
namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using DeploymentModel;
    using Tasks;
    using Tasks.WinService;

    public class ProtoWinServiceTask :
        BaseTask,
        WinServiceOptions
    {
        readonly string _serviceName;
        Action<Server> _registerAdditionalActions;

        public ProtoWinServiceTask(string serviceName)
        {
            _serviceName = serviceName;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action<Server> registerAdditionalActions)
        {
            //child task
            _registerAdditionalActions = registerAdditionalActions;

            return this;
        }

        public void Start()
        {
            
        }

        public void Stop()
        {

        }

        #endregion

        public override Task ConstructTasksForServer(DeploymentServer server)
        {
            var nest = new NestedTask();
            nest.AddTask(new WinServiceStopTask(server.Name, _serviceName));

            //child task
            //_registerAdditionalActions();

            nest.AddTask(new WinServiceStartTask(server.Name, _serviceName));
            return nest;
        }
    }
}