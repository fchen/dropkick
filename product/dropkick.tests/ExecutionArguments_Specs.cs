namespace dropkick.tests
{
    using Engine;
    using NUnit.Framework;

    [TestFixture]
    public class ExecutionArguments_Specs
    {
        string[] _arguments = new [] { "-e:staging", "-d:MyStuff.dll", "-p:WEB", "-v" };

        [Test]
        public void Should_handle_dashes_and_slashes()
        {
            var arguments = new string[] { "/e:staging", "-d:MyStuff.dll", "/p:WEB", "-v" };
            var ea = ArgumentParsing.Parse(arguments);

            Assert.AreEqual(ea.Environment, "staging");
            Assert.AreEqual(ea.Deployment, "MyStuff");
            Assert.AreEqual(ea.Command, DropkickCommands.Verify);
            Assert.AreEqual(ea.Part, "WEB");
        }

        [Test]
        public void Should_parse_out_parts()
        {
            var ea = ArgumentParsing.Parse(_arguments);

            Assert.AreEqual(ea.Part, "WEB");
        }

        [Test]
        public void Default_parts_should_be_ALL()
        {
            var ea = ArgumentParsing.Parse(new string[] {});

            Assert.AreEqual(ea.Part, "ALL");
        }

        [Test]
        public void Should_parse_out_assembly()
        {
            var ea = ArgumentParsing.Parse(_arguments);

            Assert.AreEqual(ea.Deployment, "MyStuff");
        }
        [Test]
        public void Should_parse_out_Environment()
        {
            var ea = ArgumentParsing.Parse(_arguments);
            Assert.AreEqual(ea.Environment, "staging");
        }

        [Test]
        public void Should_parse_out_verify()
        {
            var arguments = new string[] {"-v"};
            var ea = ArgumentParsing.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Verify);
        }

        [Test]
        public void Should_parse_out_execute()
        {
            var arguments = new string[] { "-x" };
            var ea = ArgumentParsing.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Execute);
        }

        [Test]
        public void Default_should_be_trace()
        {
            var arguments = new string[] {  };
            var ea = ArgumentParsing.Parse(arguments);

            Assert.AreEqual(ea.Command, DropkickCommands.Trace);
        }
    }
}