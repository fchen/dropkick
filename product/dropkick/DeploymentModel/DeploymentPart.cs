namespace dropkick.DeploymentModel
{
    using System.Collections.Generic;

    public class DeploymentPart
    {
        public DeploymentPart(string name)
        {
            Name = name;
            Details = new List<DeploymentDetail>();
        }

        public string Name { get; private set; }
        public IList<DeploymentDetail> Details { get; private set; }

        public void AddDetail(DeploymentDetail detail)
        {
            Details.Add(detail);
        }
    }
}