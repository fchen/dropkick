namespace dropkick.tests
{
    using Engine;
    using Engine.CommandLineParsing;
    using NUnit.Framework;

    [TestFixture]
    public class ExecutionArguments_Specs
    {
        string _arguments = "verify -environment:staging -deployment:MyStuff.dll -part:WEB";

        [Test]
        public void Should_handle_dashes_and_slashes()
        {
            var arguments = "verify /environment:staging -deployment:MyStuff.dll /part:WEB";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual("staging",ea.Environment);
            Assert.AreEqual("MyStuff.dll", ea.Deployment);
            Assert.AreEqual(DeploymentCommands.Verify, ea.Command);
            Assert.AreEqual("WEB", ea.Part);
        }

        [Test]
        public void Should_parse_out_parts()
        {
            var ea = DropkickCommandLineParser.Parse(_arguments);

            Assert.AreEqual(ea.Part, "WEB");
        }

        [Test]
        public void Default_parts_should_be_ALL()
        {
            var ea = DropkickCommandLineParser.Parse("");

            Assert.AreEqual(ea.Part, "ALL");
        }

        [Test]
        public void Should_parse_out_assembly()
        {
            var ea = DropkickCommandLineParser.Parse(_arguments);

            Assert.AreEqual("MyStuff.dll", ea.Deployment);
        }
        [Test]
        public void Should_parse_out_Environment()
        {
            var ea = DropkickCommandLineParser.Parse(_arguments);
            Assert.AreEqual("staging", ea.Environment);
        }

        [Test]
        public void Should_parse_out_verify()
        {
            var arguments = "verify";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DeploymentCommands.Verify);
        }

        [Test]
        public void Should_parse_out_execute()
        {
            var arguments = "execute";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DeploymentCommands.Execute);
        }
        
        [Test]
        public void Default_should_be_trace()
        {
            var arguments = "";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DeploymentCommands.Trace);
        }
    }
}