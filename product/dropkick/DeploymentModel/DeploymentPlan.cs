namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;

    //shouldn't know jack about execute/verify/trace - should just be a collection of shiz to run
    public class DeploymentPlan
    {
        readonly IList<DeploymentPart> _parts = new List<DeploymentPart>();

        public Action<DeploymentPlan> PlanAction;
        public Action<DeploymentPart> PartAction;
        public Action<DeploymentDetail> DetailAction;

        public string Name { get; set; }

        public void AddPart(DeploymentPart part)
        {
            _parts.Add(part);
        }

        public void Execute()
        {
            PlanAction(this);
            foreach (var part in _parts)
            {
                PartAction(part);

                part.ForEachDetail(DetailAction);
            }
        }



    }
}