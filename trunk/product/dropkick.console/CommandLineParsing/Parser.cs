namespace dropkick.console.CommandLineParsing
{
    using Execution;

    public interface Parser
    {
        ExecutionPlan Parse(string[] args);
    }
}