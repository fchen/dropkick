namespace dropkick.tests.TestObjects
{
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Msmq;
    using dropkick.Configuration.Dsl.WinService;

    public class WinServiceTestDeploy :
        Deployment<WinServiceTestDeploy>
    {
        public static Role Web { get; set; }
        public static Role File { get; set; }
        static WinServiceTestDeploy()
        {
            //this is just a means to check the nested closure would work, not that one would actually do this
            Define(() =>
            {
                DeploymentStepsFor(Web, r =>
                {
                                                r.WinService("MSMQ")
                                                    .Do(() => r.OnServer(System.Environment.MachineName)
                                                                  .Msmq()
                                                                  .PrivateQueueNamed("dru")
                                                                  .CreateIfItDoesntExist());
                });

                DeploymentStepsFor(File, r => r.WinService("W3SVC").Do(() => { }));

            });
        }
    }
}