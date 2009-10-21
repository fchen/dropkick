namespace dropkick.tests.TestObjects
{
    using System;
    using dropkick.Configuration.Dsl;
    using Engine;

    public class GenericDeploymentFinder<T> :
        DeploymentFinder where T : Deployment
    {
        public Deployment Find(string assemblyName)
        {
            return (Deployment)Activator.CreateInstance(typeof(T));
        }
    }
}