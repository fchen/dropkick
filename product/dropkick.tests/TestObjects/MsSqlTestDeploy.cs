namespace dropkick.tests.TestObjects
{
    using dropkick.Dsl;
    using dropkick.Dsl.MsSql;

    public class MsSqlTestDeploy :
        Deployment<MsSqlTestDeploy>
    {
        public static Part Web { get; set; }

        static MsSqlTestDeploy()
        {
            Define(() =>
                   During(Web, (p) => p.OnServer(System.Environment.MachineName)
                                          .SqlInstance(".")
                                          .Database("test")
                                          .OutputSql("SELECT * FROM Version"))
                );
        }
    }
}