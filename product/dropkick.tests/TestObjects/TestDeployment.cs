namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.Files;
    using dropkick.Dsl.Iis;
    using dropkick.Dsl.Msmq;
    using dropkick.Dsl.MsSql;
    using dropkick.Dsl.WinService;

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
                            p.OnServer("SrvTopeka19")
                                .IisSite("Exchequer")
                                .VirtualDirectory("flames")
                                .Verify()
                                .CreateIfItDoesntExist();

                            p.OnServer("SrvTopeka19")
                                .Msmq()
                                .PrivateQueueNamed("bob")
                                .CreateIfItDoesntExist();

                            p.CopyFrom(@".\code_drop\flamesweb\**\*.*").To(@"\\srvtopeka19\exchecquer\flames\")
                                .And((f) =>
                                    {
                                        f.WebConfig
                                            .ReplaceIdentityTokensWithPrompt()
                                            .EncryptIdentity();
                                    });
                        });

                    During(Db, (p) =>
                        {
                            p.OnServer("SrvTopeka02")
                                .SqlInstance(".")
                                .Database("Enterprise")
                                .Verify()
                                .BackupWithLightspeedTo(@"\\appdev\dev\sqlbacksups\")
                                .RunTarantinoOn(@".\code_drop\flames_sql");
                        });

                    During(Service, (p) =>
                        {
                            p.OnServer("SrvTopeka19")
                                .WinService("FlamesHost")
                                .Verify()
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