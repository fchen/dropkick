namespace dropkick.Engine
{
    using Configuration.Dsl;

    public interface DeploymentFinder
    {
        Deployment Find(string assemblyName);
    }
}