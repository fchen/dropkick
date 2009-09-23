namespace dropkick.Engine
{
    using Configuration.Dsl;
    using Execution;

    public static class DeploymentPlanBuilder
    {
        public static DeploymentPlan Build(Deployment deployment)
        {
            var inspector = new DropkickDeploymentInspector();
            inspector.Inspect(deployment);
            var plan = inspector.GetPlan();
            return plan;
        }
    }
}