namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.MsSql;

    public class MsSqlTestDeploy :
        Deployment<MsSqlTestDeploy>
    {
        public static Part Web { get; set; }

        static MsSqlTestDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, p =>
                   {
                       p.OnServer(System.Environment.MachineName)
                           .SqlInstance(".")
                           .Database("test")
                           .OutputSql("SELECT * FROM Version");

                       p.OnServer(System.Environment.MachineName)
                           .SqlInstance(".")
                           .Database("test")
                           .RunScript(@".\create_database.sql");
                   })

                );
        }
    }
}