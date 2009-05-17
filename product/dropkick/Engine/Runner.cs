namespace dropkick.Engine
{
    public class Runner
    {
        readonly DeploymentFinder _finder;

        public Runner(DeploymentFinder finder)
        {
            _finder = finder;
        }

        public void Deploy()
        {
            var d = _finder.Find();
            //var cfg = d.BuildCfg();
            //cfg.Run();
        }
    }
}