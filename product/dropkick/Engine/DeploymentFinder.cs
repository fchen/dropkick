namespace dropkick.Engine
{
    using Dsl;

    public interface DeploymentFinder
    {
        Deployment Find(string assemblyName);
    }
}