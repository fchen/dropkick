namespace dropkick.Engine
{
    using System;
    using System.Collections.Generic;
    using Configuration.Dsl;
    using DeploymentModel;

    public static class DeploymentPlanRunner
    {
        static readonly IDictionary<DeploymentCommands, Action<DeploymentPlan>> _actions = new Dictionary<DeploymentCommands, Action<DeploymentPlan>>();
        static readonly DropkickDeploymentInspector _inspector = new DropkickDeploymentInspector();

        static DeploymentPlanRunner()
        {
            _actions.Add(DeploymentCommands.Execute, d=>d.Execute());
            _actions.Add(DeploymentCommands.Verify,  d=>d.Verify());
            _actions.Add(DeploymentCommands.Trace,   d=>d.Trace());
        }

        public static void Build(Deployment deployment, DeploymentArguments args)
        {
            var plan = _inspector.GetPlan(deployment, args);

            _actions[args.Command](plan);
        }
    }

    public delegate void PlanAction(Deployment deployment);
    public delegate Action<DeploymentPart> PartCriteria(DeploymentPart part);
    public delegate Action<DeploymentDetail> DetailCriteria(DeploymentDetail detail);
}