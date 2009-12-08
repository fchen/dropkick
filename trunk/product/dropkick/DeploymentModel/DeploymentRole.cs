namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;

    public class DeploymentRole
    {
        readonly IList<DeploymentServer> _servers;
        readonly IList<DeploymentDetail> _details;

        public DeploymentRole(string name)
        {
            Name = name;
            _details = new List<DeploymentDetail>();
            _servers = new List<DeploymentServer>();
        }

        public string Name { get; private set; }

        public void AddDetail(DeploymentDetail detail)
        {
            _details.Add(detail);
        }

        public void AddServer(DeploymentServer server)
        {
            _servers.Add(server);
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