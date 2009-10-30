﻿// Copyright 2007-2008 The Apache Software Foundation.
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
namespace dropkick.console
{
    using System.Linq;
    using Engine;
    using log4net.Config;

    internal static class Program
    {
        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            // commands 
            //   verify     v
            //   execute    e
            //   trace      t   (default)

            // args
            //   environment    /e:local - used to work with config files
            //   part           /p:all|web - a bastardized capistrano roles
            //   deployment
            //      /d:FHLBank.Flames.Deployment.dll (an assembly)
            //      /d:FHLBank.Flames.Deployment.StandardDepoy (a class, lack of .dll)
            //      (null) - if omitted search for a dll ending with 'Deployment' then pass that name in

            //goal
            Runner.Deploy(args.Aggregate((a, b) => a + " " + b).Trim());
        }
    }
}