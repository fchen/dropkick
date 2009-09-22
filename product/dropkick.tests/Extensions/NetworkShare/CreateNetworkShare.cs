namespace dropkick.tests.Extensions.NetworkShare
{
    using System;
    using System.IO;
    using NUnit.Framework;
    using Tasks.NetworkShare;

    [TestFixture]
    public class CreateNetworkShare
    {
        [Test]
        public void Execute()
        {
            if(Directory.Exists(".\\temp"))
                Directory.CreateDirectory(".\\temp");

            var t = new FolderShareTask();
            t.PointingTo = @".\temp";
            t.ShareName = "dk_test";
            t.Server = Environment.MachineName;
            
            t.Execute();
        }

        [Test]
        public void Verify()
        {
            var t = new FolderShareTask();
            t.PointingTo = @"C:\temp";
            t.ShareName = "dk_test";
            t.Server = Environment.MachineName;

            var r = t.VerifyCanRun();
        }
    }
}