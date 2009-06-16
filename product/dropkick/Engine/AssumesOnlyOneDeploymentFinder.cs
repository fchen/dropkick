namespace dropkick.Engine
{
    using System;
    using System.Reflection;
    using Dsl;
    using System.Linq;

    public class AssumesOnlyOneDeploymentFinder :
        DeploymentFinder
    {
        readonly string _assemblyName;

        public AssumesOnlyOneDeploymentFinder(string assemblyName)
        {
            _assemblyName = assemblyName;
        }

        public Deployment Find()
        {
            Assembly asm = Assembly.Load(_assemblyName);
            var tt = asm.GetTypes().Where(t =>  typeof (Deployment).IsAssignableFrom(t) );

            return (Deployment)Activator.CreateInstance(tt.First());
        }
    }
}