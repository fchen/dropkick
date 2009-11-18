namespace dropkick.tests.TestObjects
{
    using System;
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.MsSql;

    public class MsSqlTestDeploy :
        Deployment<MsSqlTestDeploy>
    {
        static MsSqlTestDeploy()
        {
            Define(() =>
                   DeploymentStepsFor(Web, p =>
                   {
                       p.OnServer(Environment.MachineName)
                           .SqlInstance(".")
                           .Database("test")
                           .OutputSql("SELECT * FROM Version");

                       p.OnServer(Environment.MachineName)
                           .SqlInstance(".")
                           .Database("test")
                           .RunScript(@".\create_database.sql");
                   })
                );
        }

        public static Role Web { get; set; }
    }
}