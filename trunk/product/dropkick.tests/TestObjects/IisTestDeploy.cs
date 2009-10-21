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
            Define(() => DeploymentStepsFor(Web, p =>
            {
                p.OnServer(Environment.MachineName)
                    .IisSite("Default Web Site")
                    .VirtualDirectory("dk_test")
                    .CreateIfItDoesntExist();

                p.OnServer(Environment.MachineName)
                    .IisSite("Default Web Site")
                    .VirtualDirectory("fp")
                    .CreateIfItDoesntExist();
            }));
        }

        public static Part Web { get; set; }
    }
}