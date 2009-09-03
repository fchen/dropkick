namespace dropkick.Engine
{
    using System;
    using System.Reflection;
    using Configuration.Dsl;
    using System.Linq;

    public class AssumesOnlyOneDeploymentFinder :
        DeploymentFinder
    {
        public Deployment Find(string assemblyName)
        {
            Assembly asm = Assembly.Load(assemblyName);
            var tt = asm.GetTypes().Where(t =>  typeof (Deployment).IsAssignableFrom(t) );

            return (Deployment)Activator.CreateInstance(tt.First());
        }
    }
}