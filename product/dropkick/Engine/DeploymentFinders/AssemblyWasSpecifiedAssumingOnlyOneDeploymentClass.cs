namespace dropkick.Engine.DeploymentFinders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Configuration.Dsl;

    public class AssemblyWasSpecifiedAssumingOnlyOneDeploymentClass :
        DeploymentFinder
    {
        #region DeploymentFinder Members

        public Deployment Find(string assemblyName)
        {
            //check that it is an assembly
            Assembly asm = Assembly.Load(assemblyName);
            IEnumerable<Type> tt = asm.GetTypes().Where(t => typeof(Deployment).IsAssignableFrom(t));

            return new TypeWasSpecifiedAssumingItHasADefaultConstructor().Find(tt.First());
        }

        #endregion
    }
}