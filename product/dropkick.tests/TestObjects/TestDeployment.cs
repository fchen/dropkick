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

                DeploymentStepsFor(Web, server =>
                {
                    //how to get this to be the current server?

                    server.ShareFolder("bob").PointingTo(@"E:\Tools")
                        .CreateIfNotExist();

                    server.CreateDSN("NAME", "Enterprise");

                    server.CommandLine("ping")
                        .Args("www.google.com")
                        .ExecutableIsLocatedAt("");

                    server.Msmq()
                        .PrivateQueueNamed("bob")
                        .CreateIfItDoesntExist();

                    server.CopyTo(@"E:\FHLBApplications\atlas")
                        .From(@"\\someserver\bob\bill");

                    server.CopyTo(@"\\srvtopeka19\exchecquer\flames\")
                        .From(@".\code_drop\flamesweb\")
                        .With(f => f.WebConfig
                                       .ReplaceIdentityTokensWithPrompt()
                                       .EncryptIdentity());
                    //.BackupTo(path, o=>o.TimestampIt())


                    server.WinService("MSMQ").Do(() =>
                    {
                        //service stops

                        //do stuff

                        //service starts
                    });


                });

                DeploymentStepsFor(Db, server =>
                {
                    server.SqlInstance(".")
                        .Database("Enterprise")
                        .BackupWithLightspeedTo(@"\\appdev\dev\sqlbacksups\")
                        .RunTarantinoOn(@".\code_drop\flames_sql");

                });

                DeploymentStepsFor(Service, server =>
                {

                    server.WinService("FlamesHost")
                        .Do(() => //auto-stop
                        {
                            server.CopyTo(@".\code_drop\flameshost").From(@"\\srvtopeka00\whatever")
                                .With(f =>
                                {
                                    f.AppConfig
                                        .ReplaceIdentityTokensWithPrompt()
                                        .EncryptIdentity();
                                });
                        }); //auto-start   
                });
            });
        }

        public static Role Web { get; set; }
        public static Role Db { get; set; }
        public static Role Service { get; set; }
    }
}