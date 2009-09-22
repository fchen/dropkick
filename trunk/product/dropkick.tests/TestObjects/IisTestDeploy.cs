namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.Iis;

    public class IisTestDeploy :
        Deployment<IisTestDeploy>
    {
        public static Part Web { get; set; }

        static IisTestDeploy()
        {
            Define(() => DeploymentStepsFor(Web, p =>
            {
                p.OnServer(System.Environment.MachineName)
                    .IisSite("Default Web Site")
                    .VirtualDirectory("dk_test")
                    .CreateIfItDoesntExist();

                p.OnServer(System.Environment.MachineName)
                    .IisSite("Default Web Site")
                    .VirtualDirectory("fp")
                    .CreateIfItDoesntExist();
            }));
        }
    }
}