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

                DeploymentStepsFor(Web, p =>
                {
                    p.OnServer("SrvTopeka19", s =>
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
                    });



                    p.CopyFrom(@".\code_drop\flamesweb\").To(@"\\srvtopeka19\exchecquer\flames\")
                        //.BackupTo(path, o=>o.TimestampIt())
                        .And(f => f.WebConfig
                                      .ReplaceIdentityTokensWithPrompt()
                                      .EncryptIdentity());

                    p.OnServer("bob", o =>
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

                DeploymentStepsFor(Service, (p) =>
                {
                    p.OnServer("bob", o =>
                    {
                        o.WinService("FlamesHost")
                        .Do(() => //auto-stop
                        {
                            p.CopyFrom(@".\code_drop\flameshost").To(@"\\srvtopeka00\whatever")
                                .And((f) =>
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