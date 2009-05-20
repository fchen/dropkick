namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.Files;
    using dropkick.Dsl.Msmq;

    public class SinglePartDeploy :
        Deployment<SinglePartDeploy>
    {
        public static Part Web { get; set; }

        static SinglePartDeploy()
        {
            Define(() =>
                   During(Web, (p) =>
                       {
                           p.CopyFrom(".\\bob").To(".\\bill");

                           p.OnServer("srvtopeka")
                               .Msmq()
                               .PrivateQueueNamed("mt_timeout");
                       })
                );
        }
    }
}