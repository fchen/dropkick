namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.Msmq;
    using Configuration.Dsl.WinService;

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
                DeploymentStepsFor(Web, p => p.OnServer(System.Environment.MachineName)
                                     .WinService("MSMQ").Do(() => p.OnServer(System.Environment.MachineName)
                                                                      .Msmq()
                                                                      .PrivateQueueNamed("dru")
                                                                      .CreateIfItDoesntExist()));

                DeploymentStepsFor(File, p => p.OnServer(System.Environment.MachineName)
                                      .WinService("W3SVC").Do(() => { }));

            });
        }
    }
}