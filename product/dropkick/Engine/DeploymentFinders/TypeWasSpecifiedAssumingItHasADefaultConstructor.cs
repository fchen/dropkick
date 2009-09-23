namespace dropkick.Engine.DeploymentFinders
{
    using System;
    using Configuration.Dsl;

    public class TypeWasSpecifiedAssumingItHasADefaultConstructor :
        DeploymentFinder
    {
        public Deployment Find(string typeName)
        {
            var type = Type.GetType(typeName);
            return Find(type);
        }

        public Deployment Find(Type type)
        {
            return (Deployment)Activator.CreateInstance(type);
        }
    }
}