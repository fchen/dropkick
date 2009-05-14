namespace dropkick.specs
{
    using Dsl;
    using Dsl.Files;
    using Dsl.Msmq;
    using Dsl.Visitor;
    using NUnit.Framework;
    using NUnit.Framework.SyntaxHelpers;

    [TestFixture]
    public class Adding_options_via_dsl
    {
        [Test]
        public void Should_have_one_part()
        {
            var dep = new SinglePartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.That(pc.Count, Is.EqualTo(1));
        }

        [Test]
        public void Should_have_two_parts()
        {
            var dep = new TwoPartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.That(pc.Count, Is.EqualTo(2));
        }

        [Test]
        public void Trace_Test()
        {
            var dep = new SinglePartDeploy();
            var pc = new TraceVisitor();
            dep.Inspect(pc);
        }

        [Test]
        public void Verify_Test()
        {
            var dep = new SinglePartDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }
    }

    public class SinglePartDeploy :
        Deployment<SinglePartDeploy>
    {
        public static Part Web { get; set; }

        static SinglePartDeploy()
        {
            Define(() =>
                   During(Web, (p) =>
                   {
                       p.CopyFrom(".\\bob").To(".\\bill");

                       p.OnServer("srvtopeka")
                           .Msmq()
                           .PrivateQueueNamed("mt_timeout");
                   })
                );
        }
    }

    public class TwoPartDeploy :
        Deployment<TwoPartDeploy>
    {
        public static Part Web { get; set; }
        public static Part Db { get; set; }

        static TwoPartDeploy()
        {
            Define(() =>
            {
                During(Web, (p) =>
                {
                    p.CopyFrom(".\\anthony").To(".\\katie");
                });
                During(Db, (p) =>
                {
                    p.CopyFrom(".\\rob").To(".\\brandy");
                });
            });
        }
    }
}