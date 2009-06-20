namespace dropkick.Execution
{
    using System;
    using System.Collections.Generic;
    using Dsl;
    using Engine;

    public class ExecutionPlan
    {
        readonly IList<ExecutionPart> _parts = new List<ExecutionPart>();

        public string Name { get; set; }

        public void AddPart(ExecutionPart part)
        {
            _parts.Add(part);
        }

        public void Execute(ExecutionArguments arguments)
        {
            Func<ExecutionPart, bool> criteria = p => true;

            if (!arguments.Equals("ALL"))
                criteria = p => p.Name.Equals(arguments.Part);

            if (arguments.Option == ExecutionOptions.Verify)
                Verify(criteria);
            else if (arguments.Option == ExecutionOptions.Execute)
                Ex(criteria);
            else //trace
                Trace(criteria);
            
            
        }

        private void Verify(Func<ExecutionPart,bool> partCriteria)
        {
            Console.WriteLine(Name);
            foreach (var part in _parts)
            {
                if (!partCriteria(part)) continue;

                Console.WriteLine("  {0}", part.Name);
                foreach (var detail in part.Details)
                {
                    Console.WriteLine("    {0}", detail.Name);
                    var r = detail.Verify();
                    foreach (var item in r.Results)
                    {
                        Console.WriteLine("      [{0}] {1}", item.Status, item.Message);
                    }
                }
            }
        }

        private void Ex(Func<ExecutionPart, bool> partCriteria)
        {
            Console.WriteLine(Name);
            foreach (var part in _parts)
            {
                if (!partCriteria(part)) continue;

                Console.WriteLine("  {0}", part.Name);
                foreach (var detail in part.Details)
                {
                    Console.WriteLine("    {0}", detail.Name);
                    detail.Verify();
                    detail.Execute();
                }
            }
        }

        private void Trace(Func<ExecutionPart, bool> partCriteria)
        {
            Console.WriteLine(Name);
            foreach (var part in _parts)
            {
                if (!partCriteria(part)) continue;

                Console.WriteLine("  {0}", part.Name);
                foreach (var detail in part.Details)
                {
                    Console.WriteLine("    {0}", detail.Name);
                }
            }
        }
    }
}