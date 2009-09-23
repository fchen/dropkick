namespace dropkick.console.CommandLineParsing
{
    using Execution;

    public interface Parser
    {
        DeploymentPlan Parse(string[] args);
    }
}