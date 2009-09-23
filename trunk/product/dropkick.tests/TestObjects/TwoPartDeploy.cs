namespace dropkick.tests.TestObjects
{
    using Configuration.Dsl;
    using Configuration.Dsl.Files;

    public class TwoPartDeploy :
        Deployment<TwoPartDeploy>
    {
        public static Part Web { get; set; }
        public static Part Db { get; set; }

        static TwoPartDeploy()
        {
            Define(() =>
                {
                    DeploymentStepsFor(Web, (p) =>
                        {
                            p.CopyFrom(".\\anthony").To(".\\katie");
                        });
                    DeploymentStepsFor(Db, (p) =>
                        {
                            p.CopyFrom(".\\rob").To(".\\brandy");
                        });
                });
        }
    }
}