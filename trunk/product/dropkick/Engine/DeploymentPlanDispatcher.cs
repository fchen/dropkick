namespace dropkick.Engine
{
    using System;
    using System.Collections.Generic;
    using Configuration.Dsl;
    using DeploymentModel;

    public static class DeploymentPlanDispatcher
    {
        static readonly IDictionary<DeploymentCommands, Action<DeploymentPlan>> _actions = new Dictionary<DeploymentCommands, Action<DeploymentPlan>>();
        static readonly DropkickDeploymentInspector _inspector = new DropkickDeploymentInspector();

        static DeploymentPlanDispatcher()
        {
            //is this smelly?
            _actions.Add(DeploymentCommands.Execute, d=>d.Execute());
            _actions.Add(DeploymentCommands.Verify,  d=>d.Verify());
            _actions.Add(DeploymentCommands.Trace,   d=>d.Trace());
        }

        public static void KickItOutThereAlready(Deployment deployment, DeploymentArguments args)
        {
            var crit = Criteria(args);
            
            var plan = _inspector.GetPlan(deployment, crit);
            
            _actions[args.Command](plan);
        }

        static RoleCriteria Criteria(DeploymentArguments args)
        {
            //need multi-part deploys too
            if (!args.Part.Equals("ALL", StringComparison.InvariantCultureIgnoreCase))
                return p => p.Name.Equals(args.Part, StringComparison.InvariantCultureIgnoreCase);

            return p => true;
        }
    }
}