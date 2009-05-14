namespace dropkick.Dsl
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.Serialization;

    //must-inherit?
    public class Deployment<Inheritor> :
        Deployment,
        DeploymentInspectorSite
        where Inheritor : Deployment<Inheritor>
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


        private static void InitializeParts()
        {
            Type machineType = typeof(Inheritor);
            foreach (PropertyInfo propertyInfo in machineType.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if (!IsPropertyAPart(propertyInfo)) continue;

                Part<Inheritor> part = SetPropertyValue(propertyInfo, x => new Part<Inheritor>(x.Name));

                _parts.Add(part.Name, part);
            }
        }
        private static void VerifyDeploymentConfiguration()
        {
            if (_parts.Count == 0)
                throw new DeploymentException("A deployment must have at least one part to be valid.");
        }

        //initial setup to be called in the static constructor
        protected static void Define(Action definition)
        {
            definition();
        }

        

        protected static void During(Part inputPart, Action<Part> action)
        {
            Part<Inheritor> part = Part<Inheritor>.GetPart(inputPart);
            part.BindAction(action);
        }

        public static Part<Inheritor> GetPart(string name)
        {
            Part<Inheritor> state;
            return _parts.TryGetValue(name, out state) ? state : null;
        }

       
        private static TValue SetPropertyValue<TValue>(PropertyInfo propertyInfo, Func<PropertyInfo, TValue> getValue)
        {
            var value = Expression.Parameter(typeof(TValue), "value");
            var action = Expression.Lambda<Action<TValue>>(Expression.Call(propertyInfo.GetSetMethod(), value), new[] { value }).Compile();

            TValue propertyValue = getValue(propertyInfo);
            action(propertyValue);

            return propertyValue;
        }

        private static object SetPropertyValue(PropertyInfo propertyInfo, Func<PropertyInfo, object> getValue)
        {
            var value = Expression.Parameter(typeof(object), "value");
            var valueCast = propertyInfo.PropertyType.IsValueType
                                ? Expression.TypeAs(value, propertyInfo.PropertyType)
                                : Expression.Convert(value, propertyInfo.PropertyType);

            var action = Expression.Lambda<Action<object>>(Expression.Call(propertyInfo.GetSetMethod(), valueCast), new[] { value }).Compile();

            object propertyValue = getValue(propertyInfo);
            action(propertyValue);

            return propertyValue;
        }

        private static bool IsPropertyAPart(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType == typeof(Part<Inheritor>) || propertyInfo.PropertyType == typeof(Part);
        }

        public void Inspect(DeploymentInspector inspector)
        {
            inspector.Inspect(this, ()=>
            {
                foreach (Part<Inheritor> part in _parts.Values)
                {
                    part.Inspect(inspector);
                } 
            });
        }

        public void Run()
        {
            //change to visitor
            foreach (KeyValuePair<string, Part<Inheritor>> part in _parts)
            {
                part.Value.Execute();
            }
        }
    }

    public interface Deployment
    {
        void Run();
    }
}