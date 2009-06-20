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
            Console.WriteLine(Name);
            foreach (var part in _parts)
            {
                Console.WriteLine("  {0}", part.Name);
                foreach (var detail in part.Details)
                {
                    Console.WriteLine("    {0}", detail.Name);
                    var r = detail.Verify();
                    foreach (var item in r.Results)
                    {
                        Console.WriteLine("      [{0}]-{1}", item.Status, item.Message);
                    }
                }
            }
        }
    }
}