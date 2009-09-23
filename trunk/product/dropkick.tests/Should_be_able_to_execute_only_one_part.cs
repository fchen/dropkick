namespace dropkick.tests
{
    using DeploymentModel;
    using Engine;
    using NUnit.Framework;
    

    [TestFixture]
    public class Should_be_able_to_execute_only_one_part
    {
        [Test]
        public void TryWeb()
        {
            bool verifyRanDb = false;
            bool verifyRanWeb = false;
            var ew = new DeploymentDetail(()=>"", ()=>
            {
                verifyRanWeb = true;
                return new DeploymentResult();
            }, () => new DeploymentResult());
            var edb = new DeploymentDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new DeploymentResult();
            }, () => new DeploymentResult());

            
            
            var p = new DeploymentPlan();
            var web = new DeploymentPart("WEB");
            web.AddDetail(ew);

            p.AddPart(web);

            var db = new DeploymentPart("DB");
            db.AddDetail(edb);
            p.AddPart(db);

            var args = new DeploymentArguments
                       {
                           Part = "WEB",
                           Command = DropkickCommands.Verify
                       };

            p.Execute(args);

            Assert.IsTrue(verifyRanWeb);
            Assert.IsFalse(verifyRanDb);
        }

        [Test]
        public void TryDb()
        {
            bool verifyRanDb = false;
            bool verifyRanWeb = false;
            var ew = new DeploymentDetail(() => "", () =>
            {
                verifyRanWeb = true;
                return new DeploymentResult();
            }, () => new DeploymentResult());
            var edb = new DeploymentDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new DeploymentResult();
            }, () => new DeploymentResult());



            var p = new DeploymentPlan();
            var web = new DeploymentPart("WEB");
            web.AddDetail(ew);

            p.AddPart(web);

            var db = new DeploymentPart("DB");
            db.AddDetail(edb);
            p.AddPart(db);

            var args = new DeploymentArguments
            {
                Part = "DB",
                Command = DropkickCommands.Verify
            };

            p.Execute(args);

            Assert.IsFalse(verifyRanWeb);
            Assert.IsTrue(verifyRanDb);
        }
    }
}