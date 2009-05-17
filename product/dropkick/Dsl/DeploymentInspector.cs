namespace dropkick.Dsl
{
    using System;

    public interface DeploymentInspector
    {
        void Inspect(object obj);
        void Inspect(object obj, Action additionalInspections);
    }
}