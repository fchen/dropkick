namespace dropkick.tests.Dsl
{
    using dropkick.Dsl.Visitor;
    using MbUnit.Framework;
    using TestObjects;

    [TestFixture]
    public class Adding_options_via_dsl
    {
        [Test]
        public void Should_have_one_part()
        {
            var dep = new SinglePartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.AreEqual(1,pc.Count);
        }

        [Test]
        public void Should_have_two_parts()
        {
            var dep = new TwoPartDeploy();
            var pc = new PartCounter();
            dep.Inspect(pc);
            Assert.AreEqual(2, pc.Count);
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

        [Test]
        public void Verify_MsSql_Test()
        {
            var dep = new MsSqlTestDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_Test()
        {
            var dep = new SinglePartDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_MSMQ_Test()
        {
            var dep = new MsmqTestDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_SQL_Test()
        {
            var dep = new MsSqlTestDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_Command_Test()
        {
            var dep = new CommandTestDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }

        [Test]
        public void Verify_Command()
        {
            var dep = new CommandTestDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }

        [Test]
        public void Verify_WinService()
        {
            var dep = new WinServiceTestDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_Service_Test()
        {
            var dep = new WinServiceTestDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }

        [Test]
        public void Verify_Iis6()
        {
            var dep = new IisTestDeploy();
            var vi = new VerificationInspector();
            dep.Inspect(vi);
        }

        [Test, Explicit]
        public void Execute_Iis6_Test()
        {
            var dep = new IisTestDeploy();
            var vi = new ExecutionInspector();
            dep.Inspect(vi);
        }
    }
}