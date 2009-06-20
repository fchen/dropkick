namespace dropkick.tests
{
    using dropkick.Dsl;
    using Engine;
    using Execution;
    using MbUnit.Framework;
    using Rhino.Mocks;
    using Verification;
    using developwithpassion.bdd.mbunit;

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
            }, ()=>{});
            var edb = new ExecutionDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new VerificationResult();
            }, () => { });

            
            
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

            verifyRanWeb.should_be_true();
            verifyRanDb.should_be_false();
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
            }, () => { });
            var edb = new ExecutionDetail(() => "", () =>
            {
                verifyRanDb = true;
                return new VerificationResult();
            }, () => { });



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

            verifyRanWeb.should_be_false();
            verifyRanDb.should_be_true();
        }
    }
}