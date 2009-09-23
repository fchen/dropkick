namespace dropkick.Engine
{
    using System;
    using System.Text.RegularExpressions;

    //note: I have probably abused the currying of lambdas here, but hey. It was fun.
    //note: in retrospect, this kinda sucks because its hard to debug
    public static class ArgumentParsing
    {
        static readonly Regex deployRegex = new Regex(@"(-|/)d:(?<value>\w+)");
        static readonly Regex envRegex = new Regex(@"(-|/)e:(?<value>\w+)");
        static readonly Regex inspector = new Regex(@"(-|/)(?<value>v|x)");
        static readonly Regex partRegex = new Regex(@"(-|/)p:(?<value>\w+)");
        
        static readonly Action<string, ExecutionArguments> ParseItAllOut;

        static ArgumentParsing()
        {
            Action<string, ExecutionArguments, Action<string, ExecutionArguments>, Regex> curry = (s, ea, a, r) =>
                {
                    Match m = r.Match(s);
                    if (m.Success)
                        a(m.Groups["value"].Value, ea);
                };


            Action<string, ExecutionArguments, Action<string, ExecutionArguments>> setupDeployRegex = (s, ea, a) => curry(s, ea, a, deployRegex);
            Action<string, ExecutionArguments, Action<string, ExecutionArguments>> setupEnvironmentRegex = (s, ea, a) => curry(s, ea, a, envRegex);
            Action<string, ExecutionArguments, Action<string, ExecutionArguments>> setupPartRegex = (s, ea, a) => curry(s, ea, a, partRegex);
            Action<string, ExecutionArguments, Action<string, ExecutionArguments>> setupInspectorRegex = (input, ea, a) => curry(input, ea, a, inspector);

            Action<string, ExecutionArguments> setDeploymentAssembly = (s, e) => { e.DeploymentAssembly = s; };
            Action<string, ExecutionArguments> setEnvironment = (s, e) => { e.Environment = s; };
            Action<string, ExecutionArguments> setPart = (s, e) => { e.Part = s; };

            Action<string, ExecutionArguments> parseDeploymentAssembly = (s, ea) => setupDeployRegex(s, ea, setDeploymentAssembly);
            Action<string, ExecutionArguments> parseEnvironment = (s, ea) => setupEnvironmentRegex(s, ea, setEnvironment);
            Action<string, ExecutionArguments> parsePart = (s, ea) => setupPartRegex(s, ea, setPart);

            Action<string, ExecutionArguments> verify2 = (input, ea) => setupInspectorRegex(input, ea, (s, e) =>
                {
                    if(s.Equals("v")) 
                        e.Command = DropkickCommands.Verify;
                });
            Action<string, ExecutionArguments> execute2 = (input, ea) => setupInspectorRegex(input, ea, (s, e) =>
                {
                    if(s.Equals("x")) 
                        e.Command = DropkickCommands.Execute;
                });

            Action<string, ExecutionArguments> parseInspector = (input, ea) =>
                {
                    verify2(input, ea);
                    execute2(input, ea);
                };


            ParseItAllOut = (s, ea) =>
                {
                    parseDeploymentAssembly(s, ea);
                    parseEnvironment(s, ea);
                    parsePart(s, ea);
                    parseInspector(s, ea);
                };
        }

        public static ExecutionArguments Parse(string[] args)
        {
            var result = new ExecutionArguments();

            foreach (string s in args)
                ParseItAllOut(s, result);

            return result;
        }
    }
}