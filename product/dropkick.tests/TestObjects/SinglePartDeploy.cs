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
                   DeploymentStepsFor(Web, (p) =>
                   {
                       p.OnServer("srvtopeka", s =>
                       {
                           s.CopyTo(@".\bill")
                               .From(@".\bob");

                           s.Msmq()
                               .PrivateQueueNamed("mt_timeout");
                       });
                   })
                );
        }

        public static Role Web { get; set; }
    }
}