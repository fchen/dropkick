namespace dropkick.tests.Extensions.Console
{
    using DeploymentModel;
    using NUnit.Framework;
    using Tasks.CommandLine;
    

    [TestFixture]
    public class PingTest
    {
        [Test]
        public void Execute()
        {
            var t = new CommandLineTask("ping");
            t.Args = "localhost";
            t.Execute();

        }

        [Test]
        public void Verify()
        {
            var t = new CommandLineTask("ping");
            t.Args = "localhost";
            var r = t.VerifyCanRun();
            var vi = new DeploymentItem(DeploymentItemStatus.Good, "");

            Assert.AreEqual(1, r.Results.Count);
            
            //Assert.Contains(vi, r.Results);
        }
    }
}