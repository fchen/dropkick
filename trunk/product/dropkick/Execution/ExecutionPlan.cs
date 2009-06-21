namespace dropkick.Execution
{
    using System;
    using System.Collections.Generic;
    using Engine;
    using Verification;

    public class ExecutionPlan
    {
        readonly IDictionary<ExecutionOptions, Action<Func<ExecutionPart, bool>>> _actions = new Dictionary<ExecutionOptions, Action<Func<ExecutionPart, bool>>>();
        readonly IList<ExecutionPart> _parts = new List<ExecutionPart>();

        public ExecutionPlan()
        {
            _actions.Add(ExecutionOptions.Execute, Ex);
            _actions.Add(ExecutionOptions.Verify, Verify);
            _actions.Add(ExecutionOptions.Trace, Trace);
        }

        public string Name { get; set; }

        public void AddPart(ExecutionPart part)
        {
            _parts.Add(part);
        }

        public void Execute(ExecutionArguments arguments)
        {
            var criteria = BuildCriteria(arguments);

            _actions[arguments.Option](criteria);
        }

        static Func<ExecutionPart, bool> BuildCriteria(ExecutionArguments args)
        {
            Func<ExecutionPart, bool> criteria = p => true;

            if (!args.Part.Equals("ALL"))
                criteria = p => p.Name.Equals(args.Part);

            return criteria;
        }

        void Verify(Func<ExecutionPart, bool> partCriteria)
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

        void Ex(Func<ExecutionPart, bool> partCriteria)
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

        void Trace(Func<ExecutionPart, bool> partCriteria)
        {
            Walk(partCriteria,
                 plan => Console.WriteLine(plan.Name),
                 part => Console.WriteLine("  {0}", part.Name),
                 detail => Console.WriteLine("    {0}", detail.Name));
        }

        void Walk(Func<ExecutionPart, bool> partCriteria, Action<ExecutionPlan> planAction, Action<ExecutionPart> partAction, Action<ExecutionDetail> detailAction)
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