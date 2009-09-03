namespace dropkick.Configuration.Dsl
{
    using dropkick.Dsl;

    public interface DeploymentInspectorSite
    {
        void Inspect(DeploymentInspector inspector);
    }
}