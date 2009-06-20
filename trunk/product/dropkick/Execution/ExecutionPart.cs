namespace dropkick.Execution
{
    using System.Collections.Generic;

    public class ExecutionPart
    {
        public ExecutionPart(string name)
        {
            Name = name;
            Details = new List<ExecutionDetail>();
        }

        public string Name { get; private set; }
        public IList<ExecutionDetail> Details { get; private set; }

        public void AddDetail(ExecutionDetail detail)
        {
            Details.Add(detail);
        }
    }
}