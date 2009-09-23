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
        
        static readonly Action<string, DeploymentArguments> ParseItAllOut;

        static ArgumentParsing()
        {
            Action<string, DeploymentArguments, Action<string, DeploymentArguments>, Regex> curry = (s, ea, a, r) =>
                {
                    Match m = r.Match(s);
                    if (m.Success)
                        a(m.Groups["value"].Value, ea);
                };


            Action<string, DeploymentArguments, Action<string, DeploymentArguments>> setupDeployRegex = (s, ea, a) => curry(s, ea, a, deployRegex);
            Action<string, DeploymentArguments, Action<string, DeploymentArguments>> setupEnvironmentRegex = (s, ea, a) => curry(s, ea, a, envRegex);
            Action<string, DeploymentArguments, Action<string, DeploymentArguments>> setupPartRegex = (s, ea, a) => curry(s, ea, a, partRegex);
            Action<string, DeploymentArguments, Action<string, DeploymentArguments>> setupInspectorRegex = (input, ea, a) => curry(input, ea, a, inspector);

            Action<string, DeploymentArguments> setDeploymentAssembly = (s, e) => { e.Deployment = s; };
            Action<string, DeploymentArguments> setEnvironment = (s, e) => { e.Environment = s; };
            Action<string, DeploymentArguments> setPart = (s, e) => { e.Part = s; };

            Action<string, DeploymentArguments> parseDeploymentAssembly = (s, ea) => setupDeployRegex(s, ea, setDeploymentAssembly);
            Action<string, DeploymentArguments> parseEnvironment = (s, ea) => setupEnvironmentRegex(s, ea, setEnvironment);
            Action<string, DeploymentArguments> parsePart = (s, ea) => setupPartRegex(s, ea, setPart);

            Action<string, DeploymentArguments> verify2 = (input, ea) => setupInspectorRegex(input, ea, (s, e) =>
                {
                    if(s.Equals("v")) 
                        e.Command = DropkickCommands.Verify;
                });
            Action<string, DeploymentArguments> execute2 = (input, ea) => setupInspectorRegex(input, ea, (s, e) =>
                {
                    if(s.Equals("x")) 
                        e.Command = DropkickCommands.Execute;
                });

            Action<string, DeploymentArguments> parseInspector = (input, ea) =>
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

        public static DeploymentArguments Parse(string[] args)
        {
            var result = new DeploymentArguments();

            foreach (string s in args)
                ParseItAllOut(s, result);

            return result;
        }
    }
}