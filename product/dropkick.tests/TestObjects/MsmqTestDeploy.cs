namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.Msmq;

    public class MsmqTestDeploy :
        Deployment<MsmqTestDeploy>
    {
        public static Part Web { get; set; }

        static MsmqTestDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, (p) => p.OnServer(System.Environment.MachineName)
                                          .Msmq()
                                          .PrivateQueueNamed("dk_test"))
                );
        }
    }
}