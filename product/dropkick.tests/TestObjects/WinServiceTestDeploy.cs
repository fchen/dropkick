namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.WinService;
    using dropkick.Dsl.Msmq;

    public class WinServiceTestDeploy :
        Deployment<WinServiceTestDeploy>
    {
        public static Part Web { get; set; }
        public static Part File { get; set; }
        static WinServiceTestDeploy()
        {
            //this is just a means to check the nested closure would work, not that one would actually do this
            Define(() =>
            {
                During(Web, p => p.OnServer(System.Environment.MachineName)
                                     .WinService("MSMQ").Do(() => p.OnServer(System.Environment.MachineName)
                                                                      .Msmq()
                                                                      .PrivateQueueNamed("dru")
                                                                      .CreateIfItDoesntExist()));

                During(File, p => p.OnServer(System.Environment.MachineName)
                                      .WinService("W3SVC").Do(() => { }));

            });
        }
    }
}