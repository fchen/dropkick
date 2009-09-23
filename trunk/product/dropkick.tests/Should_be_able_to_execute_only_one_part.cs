namespace dropkick.tests
{
    using dropkick.Execution;
    using Engine;
    using NUnit.Framework;
    using Verification;

    [TestFixture]
    public class Should_be_able_to_execute_only_one_part
    {
        [Test]
        public void TryWeb()
        {
            bool verifyRanDb = false;
            bool verifyRanWeb = false;
            var ew = new ExecutionDetail(()=>"", ()=>
            {
                verifyRanWeb = true;
                return new VerificationResult();
            }, () => new ExecutionResult());
            var edb = new ExecutionDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new VerificationResult();
            }, () => new ExecutionResult());

            
            
            var p = new ExecutionPlan();
            var web = new ExecutionPart("WEB");
            web.AddDetail(ew);

            p.AddPart(web);

            var db = new ExecutionPart("DB");
            db.AddDetail(edb);
            p.AddPart(db);

            var args = new ExecutionArguments
                       {
                           Part = "WEB",
                           Option = ExecutionOptions.Verify
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
            var ew = new ExecutionDetail(() => "", () =>
            {
                verifyRanWeb = true;
                return new VerificationResult();
            }, () => new ExecutionResult());
            var edb = new ExecutionDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new VerificationResult();
            }, () => new ExecutionResult());



            var p = new ExecutionPlan();
            var web = new ExecutionPart("WEB");
            web.AddDetail(ew);

            p.AddPart(web);

            var db = new ExecutionPart("DB");
            db.AddDetail(edb);
            p.AddPart(db);

            var args = new ExecutionArguments
            {
                Part = "DB",
                Option = ExecutionOptions.Verify
            };

            p.Execute(args);

            Assert.IsFalse(verifyRanWeb);
            Assert.IsTrue(verifyRanDb);
        }
    }
}