namespace dropkick.Execution
{
    using System;
    using System.Collections.Generic;
    using Engine;
    using Verification;

    public class DeploymentPlan
    {
        readonly IDictionary<ExecutionOptions, Action<Func<DeploymentPart, bool>>> _actions = new Dictionary<ExecutionOptions, Action<Func<DeploymentPart, bool>>>();
        readonly IList<DeploymentPart> _parts = new List<DeploymentPart>();

        public DeploymentPlan()
        {
            _actions.Add(ExecutionOptions.Execute, Ex);
            _actions.Add(ExecutionOptions.Verify, Verify);
            _actions.Add(ExecutionOptions.Trace, Trace);
        }

        public string Name { get; set; }

        public void AddPart(DeploymentPart part)
        {
            _parts.Add(part);
        }

        public void Execute(ExecutionArguments arguments)
        {
            var criteria = BuildCriteria(arguments);

            _actions[arguments.Option](criteria);
        }

        static Func<DeploymentPart, bool> BuildCriteria(ExecutionArguments args)
        {
            Func<DeploymentPart, bool> criteria = p => true;

            if (!args.Part.Equals("ALL"))
                criteria = p => p.Name.Equals(args.Part);

            return criteria;
        }

        void Verify(Func<DeploymentPart, bool> partCriteria)
        {
            Walk(partCriteria,
                 plan => Console.WriteLine(plan.Name),
                 part => Console.WriteLine("  {0}", part.Name),
                 detail =>
                 {
                     Console.WriteLine("    {0}", detail.Name);
                     VerificationResult r = detail.Verify();
                     foreach (VerificationItem item in r.Results)
                     {
                         Console.WriteLine("      [{0}] {1}", item.Status, item.Message);
                     }
                 });
        }

        void Ex(Func<DeploymentPart, bool> partCriteria)
        {
            Walk(partCriteria,
                 plan => Console.WriteLine(plan.Name),
                 part => Console.WriteLine("  {0}", part.Name),
                 detail =>
                 {
                     Console.WriteLine("    {0}", detail.Name);
                     detail.Verify();
                     detail.Execute();
                 });
        }

        void Trace(Func<DeploymentPart, bool> partCriteria)
        {
            Walk(partCriteria,
                 plan => Console.WriteLine(plan.Name),
                 part => Console.WriteLine("  {0}", part.Name),
                 detail => Console.WriteLine("    {0}", detail.Name));
        }

        void Walk(Func<DeploymentPart, bool> partCriteria, Action<DeploymentPlan> planAction, Action<DeploymentPart> partAction, Action<DeploymentDetail> detailAction)
        {
            planAction(this);
            foreach (var part in _parts)
            {
                if (!partCriteria(part)) continue;
                partAction(part);
                foreach (var detail in part.Details)
                {
                    detailAction(detail);
                }
            }
        }
    }
}