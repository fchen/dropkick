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
                   DeploymentStepsFor(Web, server => server.CommandLine("ipconfig"))
                );
        }

        public static Role Web { get; set; }
    }
}