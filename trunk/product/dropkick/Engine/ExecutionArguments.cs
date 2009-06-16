namespace dropkick.Engine
{
    using System;
    using System.Text.RegularExpressions;
    using Dsl;
    using Visitors.Trace;

    public class ExecutionArguments
    {
        public ExecutionArguments()
        {
            Inspector = new TraceVisitor();
        }
        public string Environment { get; set; }
        public string Part { get; set; }
        public string DeploymentAssembly { get; set; }
        public DeploymentInspector Inspector { get; set; }
    }
}