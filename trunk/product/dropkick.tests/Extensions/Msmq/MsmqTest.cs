namespace dropkick.tests.Extensions.Msmq
{
    using System;
    using Configuration.Dsl.Msmq;
    using NUnit.Framework;

    [TestFixture]
    public class MsmqTest
    {
        [Test]
        public void Execute()
        {
            var t = new MsmqTask();
            t.QueueName = "dk_test";
            t.ServerName = Environment.MachineName;
            t.CreateIfItDoesNotExist();

            t.Execute();
        }

        [Test]
        public void Verify()
        {
            var t = new MsmqTask();
            t.QueueName = "dk_test";
            t.ServerName = Environment.MachineName;
            

            var r = t.VerifyCanRun();
            
        }
        
    }
}