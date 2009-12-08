namespace dropkick.tests.TestObjects
{
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Files;
    using dropkick.Configuration.Dsl.Msmq;

    public class SinglePartDeploy :
        Deployment<SinglePartDeploy>
    {
        static SinglePartDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, server =>
                   {
                       server.CopyTo(@".\bill")
                           .From(@".\bob");

                       server.Msmq()
                           .PrivateQueueNamed("mt_timeout");

                   })
                );
        }

        public static Role Web { get; set; }
    }
}