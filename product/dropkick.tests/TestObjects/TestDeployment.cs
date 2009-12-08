namespace dropkick.tests.TestObjects
{
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.CommandLine;
    using dropkick.Configuration.Dsl.Dsn;
    using dropkick.Configuration.Dsl.Files;
    using dropkick.Configuration.Dsl.Msmq;
    using dropkick.Configuration.Dsl.MsSql;
    using dropkick.Configuration.Dsl.NetworkShare;
    using dropkick.Configuration.Dsl.WinService;

    public class TestDeployment :
        Deployment<TestDeployment>
    {
        static TestDeployment()
        {
            Define(() =>
            {
                //Could be on FhlbDeployment
                //EnableTripwireReporting();         //file copies and movies are evented

                DeploymentStepsFor(Web, r =>
                {

                    r.CopyTo(@"E:\FHLBApplications\atlas")
                        .From(@"\\someserver\bob\bill");

                    r.CopyTo(@"\\srvtopeka19\exchecquer\flames\")
                        .From(@".\code_drop\flamesweb\")
                        .With(f => f.WebConfig
                                  .ReplaceIdentityTokensWithPrompt()
                                  .EncryptIdentity());

                    //how to get this to be the current server?
                    r.OnServer("SrvTopeka19", s =>
                    {
                        s.ShareFolder("bob").PointingTo(@"E:\Tools")
                            .CreateIfNotExist();

                        s.CreateDSN("NAME", "Enterprise");

                        s.CommandLine("ping")
                            .Args("www.google.com")
                            .ExecutableIsLocatedAt("");

                        s.Msmq()
                            .PrivateQueueNamed("bob")
                            .CreateIfItDoesntExist();

                        //.BackupTo(path, o=>o.TimestampIt())
                    });
                        

                    r.OnServer("bob", o =>
                    {
                        o.WinService("MSMQ").Do(() =>
                        {
                            //service stops

                            //do stuff

                            //service starts
                        });
                    });
                });

                DeploymentStepsFor(Db, (p) =>
                {
                    p.OnServer("", o =>
                    {
                        o.SqlInstance(".")
                            .Database("Enterprise")
                            .BackupWithLightspeedTo(@"\\appdev\dev\sqlbacksups\")
                            .RunTarantinoOn(@".\code_drop\flames_sql");
                    });
                });

                DeploymentStepsFor(Service, p =>
                {
                    p.OnServer("bob", o =>
                    {
                        o.WinService("FlamesHost")
                        .Do(() => //auto-stop
                        {
                            p.CopyTo(@".\code_drop\flameshost").From(@"\\srvtopeka00\whatever")
                                .With(f =>
                                {
                                    f.AppConfig
                                        .ReplaceIdentityTokensWithPrompt()
                                        .EncryptIdentity();
                                });
                        }); //auto-start
                    });    
                });
            });
        }

        public static Role Web { get; set; }
        public static Role Db { get; set; }
        public static Role Service { get; set; }
    }
}