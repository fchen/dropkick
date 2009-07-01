namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.CommandLine;
    using dropkick.Dsl.Dsn;
    using dropkick.Dsl.Files;
    using dropkick.Dsl.Iis;
    using dropkick.Dsl.Msmq;
    using dropkick.Dsl.MsSql;
    using dropkick.Dsl.WinService;
    using dropkick.Dsl.NetworkShare;

    public class TestDeployment :
        Deployment<TestDeployment>
    {
        public static Part Web { get; set; }
        public static Part Db { get; set; }
        public static Part Service { get; set; }

        //dropkick r:role(web,db,service | full)
        static TestDeployment()
        {
            Define(() =>
                {
                    //Could be on FhlbDeployment
                    //EnableTripwireReporting();         //file copies and movies are evented

                    During(Web, (p) =>
                        {
//                            p.OnServer("SrvTopeka19")
//                                .Iis6Site("Exchequer")
//                                .VirtualDirectory("flames")
//                                .CreateIfItDoesntExist();

                            
                              
                              p.OnServer("SrvTopeka19")
                                   .ShareFolder("bob").PointingTo(@"E:\Tools")
                                   .CreateIfNotExist();

                            p.OnServer("SrvTopeka19")
                                .CreateDSN("NAME");

                            p.OnServer("SrvTopeka19")
                                .CommandLine("ping")
                                .Args("www.google.com")
                                .ExecutableIsLocatedAt("");

                            p.OnServer("SrvTopeka19")
                                .Msmq()
                                .PrivateQueueNamed("bob")
                                .CreateIfItDoesntExist();

                            p.CopyFrom(@".\code_drop\flamesweb\").To(@"\\srvtopeka19\exchecquer\flames\")//.BackupTo(path, o=>o.TimestampIt())
                                .And(f => f.WebConfig
                                              .ReplaceIdentityTokensWithPrompt()
                                              .EncryptIdentity());
                            p.OnServer("SrvTopeka02")
                                .WinService("MSMQ").Do(() =>
                                                           {
                                                               // do stuff here like copy files
                                                           });
                        });

                    During(Db, (p) =>
                        {
                            p.OnServer("SrvTopeka02")
                                .SqlInstance(".")
                                .Database("Enterprise")
                                .BackupWithLightspeedTo(@"\\appdev\dev\sqlbacksups\")
                                .RunTarantinoOn(@".\code_drop\flames_sql");
                        });

                    During(Service, (p) =>
                        {
                            p.OnServer("SrvTopeka19")
                                .WinService("FlamesHost")
                                .Do(() =>   //auto-stop
                                    {
                                        p.CopyFrom(@".\code_drop\flameshost").To(@"\\srvtopeka00\whatever")
                                            .And((f) =>
                                                {
                                                    f.AppConfig
                                                        .ReplaceIdentityTokensWithPrompt()
                                                        .EncryptIdentity();
                                                });
                                    });        //auto-start
                        });

                });
        }
    }
}