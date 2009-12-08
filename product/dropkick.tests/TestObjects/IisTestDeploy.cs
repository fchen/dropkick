namespace dropkick.tests.TestObjects
{
    using System;
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Iis;

    public class IisTestDeploy :
        Deployment<IisTestDeploy>
    {
        static IisTestDeploy()
        {
            Define(() => DeploymentStepsFor(Web, server =>
            {
                server.IisSite("Default Web Site")
                    .VirtualDirectory("dk_test")
                    .CreateIfItDoesntExist();

                server.IisSite("Default Web Site")
                    .VirtualDirectory("fp")
                    .CreateIfItDoesntExist();
            }));
        }

        public static Role Web { get; set; }
    }
}