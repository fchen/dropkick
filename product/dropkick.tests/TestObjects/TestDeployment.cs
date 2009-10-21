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
//                            p.OnServer("SrvTopeka19")
//                                .Iis6Site("Exchequer")
//                                .VirtualDirectory("flames")
//                                .CreateIfItDoesntExist();


                    p.OnServer("SrvTopeka19")
                        .ShareFolder("bob").PointingTo(@"E:\Tools")
                        .CreateIfNotExist();

                    p.OnServer("SrvTopeka19")
                        .CreateDSN("NAME", "Enterprise");

                    p.OnServer("SrvTopeka19")
                        .CommandLine("ping")
                        .Args("www.google.com")
                        .ExecutableIsLocatedAt("");

                    p.OnServer("SrvTopeka19")
                        .Msmq()
                        .PrivateQueueNamed("bob")
                        .CreateIfItDoesntExist();

                    p.CopyFrom(@".\code_drop\flamesweb\").To(@"\\srvtopeka19\exchecquer\flames\")
                        //.BackupTo(path, o=>o.TimestampIt())
                        .And(f => f.WebConfig
                                      .ReplaceIdentityTokensWithPrompt()
                                      .EncryptIdentity());
                    p.OnServer("SrvTopeka02")
                        .WinService("MSMQ").Do(() =>
                        {
                            // do stuff here like copy files
                        });
                });

                DeploymentStepsFor(Db, (p) =>
                {
                    p.OnServer("SrvTopeka02")
                        .SqlInstance(".")
                        .Database("Enterprise")
                        .BackupWithLightspeedTo(@"\\appdev\dev\sqlbacksups\")
                        .RunTarantinoOn(@".\code_drop\flames_sql");
                });

                DeploymentStepsFor(Service, (p) =>
                {
                    p.OnServer("SrvTopeka19")
                        .WinService("FlamesHost")
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
        }

        public static Part Web { get; set; }
        public static Part Db { get; set; }
        public static Part Service { get; set; }
    }
}