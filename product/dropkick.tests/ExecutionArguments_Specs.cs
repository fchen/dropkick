namespace dropkick.tests
{
    using Engine;
    using MbUnit.Framework;
    using developwithpassion.bdd.mbunit;
    using Visitors.Execution;
    using Visitors.Trace;
    using Visitors.Verification;

    [TestFixture]
    public class ExecutionArguments_Specs
    {
        [Test]
        public void Verify()
        {
            var arguments = new string[] {"-e:staging", "-d:MyStuff.dll","-v"};
            var ea = ArgumentParsing.Parse(arguments);

            ea.Environment.should_be_equal_to("staging");
            ea.DeploymentAssembly.should_be_equal_to("MyStuff");
            ea.Inspector.should_be_an_instance_of<VerificationInspector>();
        }

        [Test]
        public void Execute()
        {
            var arguments = new string[] { "-e:staging", "-d:MyStuff.dll", "-x" };
            var ea = ArgumentParsing.Parse(arguments);

            ea.Environment.should_be_equal_to("staging");
            ea.DeploymentAssembly.should_be_equal_to("MyStuff");
            ea.Inspector.should_be_an_instance_of<ExecutionInspector>();
        }

        [Test]
        public void Trace()
        {
            var arguments = new string[] { "-e:staging", "-d:MyStuff.dll" };
            var ea = ArgumentParsing.Parse(arguments);

            ea.Environment.should_be_equal_to("staging");
            ea.DeploymentAssembly.should_be_equal_to("MyStuff");
            ea.Inspector.should_be_an_instance_of<TraceVisitor>();
        }
    }
}