namespace dropkick.Engine
{
    using System;
    using Dsl;

    public class GenericDeploymentFinder<T> :
        DeploymentFinder where T : Deployment
    {
        public Deployment Find(string assemblyName)
        {
            return (Deployment)Activator.CreateInstance(typeof(T));
        }
    }
}