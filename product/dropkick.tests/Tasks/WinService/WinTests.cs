namespace dropkick.tests.Tasks.WinService
{
    using System;
    using dropkick.Configuration.Dsl;
    using dropkick.Tasks.WinService;
    using NUnit.Framework;

    [TestFixture]
    public class WinTests
    {
        [Test][Category("Integration")]
        public void Start()
        {
            //TODO: friggin 2008 LUA-must run as admin
            Role role = null; //TODO: Fix
            var t = new WinServiceStopTask(role,"MSMQ");
            var o  = t.VerifyCanRun();
            t.Execute();
            var t2 = new WinServiceStartTask(role, "MSMQ");
            t.Execute();

        }
    }
}