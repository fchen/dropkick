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
            Assert.AreEqual(DropkickCommands.Verify, ea.Command);
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
            var ea = DropkickCommandLineParser.Parse("-deployment:bob.dll");

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
            var arguments = "verify -deployment:d";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Verify);
        }

        [Test]
        public void Should_parse_out_execute()
        {
            var arguments = "execute -deployment:d";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Execute);
        }
        
        [Test]
        public void Default_should_be_trace()
        {
            var arguments = "-deployment:d";
            var ea = DropkickCommandLineParser.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Trace);
        }
    }
}