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
                       p.CopyFrom(@".\bob").To(@".\bill");

                       p.OnServer("srvtopeka")
                           .Msmq()
                           .PrivateQueueNamed("mt_timeout");
                   })
                );
        }

        public static Part Web { get; set; }
    }
}