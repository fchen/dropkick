namespace dropkick.tests.Configuration.Dsl
{
    using dropkick.Configuration.Dsl;
    using Engine;
    using NUnit.Framework;
    using TestObjects;

    [TestFixture]
    public abstract class WithTwoPartDeployContext
    {
        public TwoPartDeploy Deployment { get; private set; }
        public DropkickDeploymentInspector Inspector { get; private set; }
        public RoleToServerMap Map { get; private set; }

        [TestFixtureSetUp]
        public void EstablishContext()
        {
            Deployment = new TwoPartDeploy();
            Inspector = new DropkickDeploymentInspector();
            Map = new RoleToServerMap();
            Map.AddMap("Web","SrvTopeka09");
            Map.AddMap("Web","SrvTopeka19");
            Map.AddMap("Db","SrvTopeka02");

            BecauseOf();
        }

        public abstract void BecauseOf();
    }
}