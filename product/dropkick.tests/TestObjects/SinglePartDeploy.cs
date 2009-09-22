namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.Files;
    using Configuration.Dsl.Msmq;

    public class SinglePartDeploy :
        Deployment<SinglePartDeploy>
    {
        public static Part Web { get; set; }

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
    }
}