namespace dropkick.Dsl
{
    using System;

    public interface DeploymentInspector
    {
        void Inspect(object obj);
        void Inspect(object obj, ExposeMoreInspectionSites additionalInspections);
    }

    public delegate void ExposeMoreInspectionSites();
}