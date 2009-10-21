namespace dropkick.tests.TestObjects
{
    using System;
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.CommandLine;

    public class CommandTestDeploy :
        Deployment<CommandTestDeploy>
    {
        static CommandTestDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, (p) =>
                   {
                       p.OnServer(Environment.MachineName)
                           .CommandLine("ipconfig");
//                       p.OnServer(System.Environment.MachineName)
//                           .CommandLine("ping").Args("www.google.com")
//                           .ExecutableIsLocatedAt(@"C:\bill");
                   })
                );
        }

        public static Part Web { get; set; }
    }
}