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
namespace dropkick.Configuration.Dsl.Iis
{
    using Tasks.Iis;

    public class Iis6TaskCfg :
        IisSiteOptions,
        IisVirtualDirectoryOptions
    {
        readonly Server _server;
        readonly Iis6Task _task;

        public Iis6TaskCfg(Server server, string websiteName)
        {
            _server = server;
            _task = new Iis6Task
                    {
                        ServerName = server.Name,
                        WebsiteName = websiteName
                    };
            server.RegisterTask(_task);
        }

        #region IisSiteOptions Members

        public IisVirtualDirectoryOptions VirtualDirectory(string name)
        {
            _task.VdirPath = name;
            _server.RegisterTask(_task);
            return this;
        }

        #endregion

        #region IisVirtualDirectoryOptions Members

        public void CreateIfItDoesntExist()
        {
            _task.CreateIfItDoesntExist();
        }

        #endregion
    }
}