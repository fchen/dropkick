namespace dropkick.tests
{
    using Engine;
    using MbUnit.Framework;
    using developwithpassion.bdd.mbunit;

    [TestFixture]
    public class ExecutionArguments_Specs
    {
        string[] _arguments = new [] { "-e:staging", "-d:MyStuff.dll", "-p:WEB", "-v" };

        [Test]
        public void Should_handle_dashes_and_slashes()
        {
            var arguments = new string[] { "/e:staging", "-d:MyStuff.dll", "/p:WEB", "-v" };
            var ea = ArgumentParsing.Parse(arguments);

            ea.Environment.should_be_equal_to("staging");
            ea.DeploymentAssembly.should_be_equal_to("MyStuff");
            ea.Option.should_be_equal_to(ExecutionOptions.Verify);
            ea.Part.should_be_equal_to("WEB");
        }

        [Test]
        public void Should_parse_out_parts()
        {
            var ea = ArgumentParsing.Parse(_arguments);

            ea.Part.should_be_equal_to("WEB");
        }

        [Test]
        public void Default_parts_should_be_ALL()
        {
            var ea = ArgumentParsing.Parse(new string[] {});

            ea.Part.should_be_equal_to("ALL");
        }

        [Test]
        public void Should_parse_out_assembly()
        {
            var ea = ArgumentParsing.Parse(_arguments);

            ea.DeploymentAssembly.should_be_equal_to("MyStuff");
        }
        [Test]
        public void Should_parse_out_Environment()
        {
            var ea = ArgumentParsing.Parse(_arguments);
            ea.Environment.should_be_equal_to("staging");
        }

        [Test]
        public void Should_parse_out_verify()
        {
            var arguments = new string[] {"-v"};
            var ea = ArgumentParsing.Parse(arguments);

            ea.Option.should_be_equal_to(ExecutionOptions.Verify);
        }

        [Test]
        public void Should_parse_out_execute()
        {
            var arguments = new string[] { "-x" };
            var ea = ArgumentParsing.Parse(arguments);

            ea.Option.should_be_equal_to(ExecutionOptions.Execute);
        }

        [Test]
        public void Default_should_be_trace()
        {
            var arguments = new string[] {  };
            var ea = ArgumentParsing.Parse(arguments);

            ea.Option.should_be_equal_to(ExecutionOptions.Trace);
        }
    }
}