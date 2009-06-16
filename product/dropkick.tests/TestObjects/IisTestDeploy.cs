namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.Iis;

    public class IisTestDeploy :
        Deployment<IisTestDeploy>
    {
        public static Part Web { get; set; }

        static IisTestDeploy()
        {
            Define(() => During(Web, p =>
            {
                p.OnServer(System.Environment.MachineName)
                    .Iis6Site("Default Web Site")
                    .VirtualDirectory("dk_test")
                    .CreateIfItDoesntExist();

                p.OnServer(System.Environment.MachineName)
                    .Iis6Site("Default Web Site")
                    .VirtualDirectory("fp")
                    .CreateIfItDoesntExist();
            }));
        }
    }
}