namespace dropkick.tests.TestObjects
{
    using System;
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Msmq;

    public class MsmqTestDeploy :
        Deployment<MsmqTestDeploy>
    {
        static MsmqTestDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, (p) => p.OnServer(Environment.MachineName)
                                                      .Msmq()
                                                      .PrivateQueueNamed("dk_test"))
                );
        }

        public static Part Web { get; set; }
    }
}