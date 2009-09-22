using dropkick.Tasks.Dsn;

namespace dropkick.tests.Extensions.Dsn
{
    using System;
    using Configuration.Dsl.Dsn;
    using NUnit.Framework;

    [TestFixture]
    public class CreatingDsns
    {
        [Test]
        public void Execute()
        {
            var t = new DsnTask(Environment.MachineName, "dk_dsn", DsnAction.AddSystemDsn, DsnDriver.Sql(), "Test");
            t.Execute();
        }

        [Test]
        public void Verify()
        {
            var t = new DsnTask(Environment.MachineName, "dk_dsn", DsnAction.AddSystemDsn, DsnDriver.Sql(), "Test");
            var r = t.VerifyCanRun();
            Assert.AreEqual(1, r.Results.Count);
        }
    }
}