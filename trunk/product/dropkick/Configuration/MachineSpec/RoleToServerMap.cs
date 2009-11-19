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
namespace dropkick.Configuration.MachineSpec
{
    using System.Collections.Generic;
    using Magnum.Collections;

    public class RoleToServerMap
    {
        readonly MultiDictionary<string, ServerInfo> _mappings;

        public RoleToServerMap()
        {
            _mappings = new MultiDictionary<string, ServerInfo>(false);
        }

        public void AddMap(string roleName, string serverName)
        {
            _mappings.Add(roleName, new ServerInfo(serverName));
        }

        public ICollection<ServerInfo> GetServers(string roleName)
        {
            return _mappings[roleName];
        }
    }

    public class ServerInfo
    {
        public ServerInfo(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public bool IsLocal
        {
            get
            {
                return System.Environment.MachineName == Name;
            }
        }

        public bool Equals(ServerInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(ServerInfo)) return false;
            return Equals((ServerInfo) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}