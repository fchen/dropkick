namespace dropkick.Engine
{
    using System;
    using System.Collections.Generic;
    using Configuration.Dsl;
    using DeploymentModel;

    public static class DeploymentPlanBuilder
    {
        static readonly IDictionary<DeploymentCommands, Action<DeploymentPlan>> _actions = new Dictionary<DeploymentCommands, Action<DeploymentPlan>>();
        static readonly DropkickDeploymentInspector _inspector = new DropkickDeploymentInspector();

        static DeploymentPlanBuilder()
        {
            _actions.Add(DeploymentCommands.Execute, Ex);
            _actions.Add(DeploymentCommands.Verify, Verify);
            _actions.Add(DeploymentCommands.Trace, Trace);
        }

        public static DeploymentPlan Build(Deployment deployment, DeploymentArguments args)
        {
            //walks the DSL?
            var plan = _inspector.GetPlan(deployment, args);
            
            //this should go in with the 'GetPlan'
            //plan.Action = _actions[args.Command];
            _actions[args.Command](plan);

            return plan;
        }

        static void Verify(DeploymentPlan plan)
        {
            plan.PlanAction = p => Console.WriteLine(p.Name);

            plan.PartAction = p => Console.WriteLine("  {0}", p.Name);

            plan.DetailAction = detail =>
            {
                Console.WriteLine("    {0}", detail.Name);
                var r = detail.Verify();
                foreach (var item in r.Results)
                {
                    Console.WriteLine("      [{0}] {1}", item.Status, item.Message);
                }
            };
        }
        static void Ex(DeploymentPlan plan)
        {
            plan.PlanAction = p => Console.WriteLine(p.Name);
            plan.PartAction = part => Console.WriteLine("  {0}", part.Name);
            plan.DetailAction = detail =>
            {
                Console.WriteLine("    {0}", detail.Name);
                detail.Verify();
                detail.Execute();
            };
        }
        static void Trace(DeploymentPlan plan)
        {
            plan.PlanAction =
                p => Console.WriteLine(p.Name);

            plan.PartAction = part => Console.WriteLine("  {0}", part.Name);
            plan.DetailAction = detail => Console.WriteLine("    {0}", detail.Name);
        }
    }

    public delegate void PlanAction(Deployment deployment);
    public delegate Action<DeploymentPart> PartCriteria(DeploymentPart part);
    public delegate Action<DeploymentDetail> DetailCriteria(DeploymentDetail detail);
}