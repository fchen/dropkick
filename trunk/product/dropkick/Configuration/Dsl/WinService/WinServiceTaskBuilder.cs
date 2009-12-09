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
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly Server _server;
        readonly string _serviceName;

        public WinServiceTaskBuilder(Server server, string name)
        {
            _serviceName = name;
            _server = server;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action registerAdditionalActions)
        {
            _server.RegisterTask(new WinServiceStopTask(_server.Name, _serviceName));

            //child task
            registerAdditionalActions();

            _server.RegisterTask(new WinServiceStartTask(_server.Name, _serviceName));

            return this;
        }

        #endregion
    }
}