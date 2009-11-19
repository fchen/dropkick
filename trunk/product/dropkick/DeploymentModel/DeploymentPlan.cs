namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;

    public class DeploymentPlan
    {
        readonly IList<DeploymentRole> _roles = new List<DeploymentRole>();

        public string Name { get; set; }

        public int PartCount
        {
            get { return _roles.Count; }
        }

        public void AddPart(DeploymentRole role)
        {
            _roles.Add(role);
        }

        public DeploymentResult Execute()
        {
            return Ex(d=>
            {
                var o = d.Verify();
                var oo = d.Execute();

                return o.MergedWith(oo);
            });
        }
        public DeploymentResult Verify()
        {
            return Ex(d=>d.Verify());
        }
        public DeploymentResult Trace()
        {
            return Ex(d=> new DeploymentResult());
        }

        DeploymentResult Ex(Func<DeploymentDetail, DeploymentResult> action)
        {
            Console.WriteLine(Name);
            var result = new DeploymentResult();

            foreach (var role in _roles)
            {
                Console.WriteLine("  {0}", role.Name);

                role.ForEachDetail(d=>
                {
                    Console.WriteLine("    {0}", d.Name);
                    var r = action(d);
                    result.MergedWith(r);
                    foreach (var item in r.Results)
                    {
                        Console.WriteLine("      [{0}] {1}", item.Status, item.Message);
                    }
                });
            }

            return result;
        }

    }
}