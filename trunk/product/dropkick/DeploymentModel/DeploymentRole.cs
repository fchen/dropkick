namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;

    public class DeploymentRole
    {
        readonly IList<DeploymentDetail> _details;

        public DeploymentRole(string name)
        {
            Name = name;
            _details = new List<DeploymentDetail>();
        }

        public string Name { get; private set; }

        public void AddDetail(DeploymentDetail detail)
        {
            _details.Add(detail);
        }

        public void ForEachDetail(Action<DeploymentDetail> detailAction)
        {
            foreach (var detail in _details)
            {
                detailAction(detail);
            }
        }
    }
}