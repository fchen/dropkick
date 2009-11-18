namespace dropkick.Configuration.Dsl
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public interface Deployment :
        DeploymentInspectorSite
    {
    }

    public class Deployment<Inheritor> :
        Deployment
        where Inheritor : Deployment<Inheritor>, new()
    {
        static readonly Dictionary<string, Part<Inheritor>> _parts = new Dictionary<string, Part<Inheritor>>();

        static Deployment()
        {
            InitializeParts();
        }

        protected Deployment()
        {
            VerifyDeploymentConfiguration();
        }

        static void InitializeParts()
        {
            Type machineType = typeof(Inheritor);

            foreach(PropertyInfo propertyInfo in machineType.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if(IsNotAPart(propertyInfo)) continue;

                Part<Inheritor> part = SetPropertyValue(propertyInfo, x => new Part<Inheritor>(x.Name));

                _parts.Add(part.Name, part);
            }
        }

        static void VerifyDeploymentConfiguration()
        {
            if(_parts.Count == 0)
                throw new DeploymentConfigurationException("A deployment must have at least one part to be valid.");
        }

        //initial setup to be called in the static constructor
        protected static void Define(Action definition)
        {
            definition();
        }

        //needs to be renamed
        protected static void DeploymentStepsFor(Role inputRole, Action<Role> action)
        {
            Part<Inheritor> part = Part<Inheritor>.GetPart(inputRole);
            part.BindAction(action);
        }

        public static Part<Inheritor> GetPart(string name)
        {
            Part<Inheritor> state;
            return _parts.TryGetValue(name, out state) ? state : null;
        }

        static TValue SetPropertyValue<TValue>(PropertyInfo propertyInfo, Func<PropertyInfo, TValue> getValue)
        {
            var value = Expression.Parameter(typeof(TValue), "value");
            var action = Expression.Lambda<Action<TValue>>(Expression.Call(propertyInfo.GetSetMethod(), value), new[] {value}).Compile();

            TValue propertyValue = getValue(propertyInfo);
            action(propertyValue);

            return propertyValue;
        }

        static bool IsNotAPart(PropertyInfo propertyInfo)
        {
            return !(propertyInfo.PropertyType == typeof(Part<Inheritor>) || propertyInfo.PropertyType == typeof(Role));
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this, () =>
            {
                foreach(Part<Inheritor> part in _parts.Values)
                {
                    part.Inspect(inspector);
                }
            });
        }
    }
}