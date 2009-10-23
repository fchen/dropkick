namespace dropkick.console
{
    using System;
    using Engine;

    internal static class Program
    {
        static void Main(string[] args)
        {
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
            Runner.Deploy(Environment.CommandLine);
        }
    }
}