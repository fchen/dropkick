namespace dropkick.tests.TestObjects
{
    using dropkick.Configuration.Dsl;
    using dropkick.Configuration.Dsl.Files;

    public class TwoPartDeploy :
        Deployment<TwoPartDeploy>
    {
        public static Role Web { get; set; }
        public static Role Db { get; set; }

        static TwoPartDeploy()
        {
            Define(() =>
                {
                    DeploymentStepsFor(Web, p =>
                        {
                            p.CopyFrom(".\\anthony").To(".\\katie");
                        });
                    DeploymentStepsFor(Db, p =>
                        {
                            p.CopyFrom(".\\rob").To(".\\brandy");
                        });
                });
        }
    }
}