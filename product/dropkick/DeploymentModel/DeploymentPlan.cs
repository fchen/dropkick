namespace dropkick.DeploymentModel
{
    using System;
    using System.Collections.Generic;

    public class DeploymentPlan
    {
        readonly IList<DeploymentPart> _parts = new List<DeploymentPart>();

        public string Name { get; set; }

        public void AddPart(DeploymentPart part)
        {
            _parts.Add(part);
        }

        public void Execute()
        {
            Ex(d=>
            {
                var o = d.Verify();
                var oo = d.Execute();

                return o.MergedWith(oo);
            });
        }
        public void Verify()
        {
            Ex(d=>d.Verify());
        }
        public void Trace()
        {
            Ex(d=> new DeploymentResult());
        }

        void Ex(Func<DeploymentDetail, DeploymentResult> action)
        {
            Console.WriteLine(Name);
        
            foreach (var part in _parts)
            {
                Console.WriteLine("  {0}", part.Name);

                part.ForEachDetail(d=>
                {
                    Console.WriteLine("    {0}", d.Name);
                    var r = action(d);
                    foreach (var item in r.Results)
                    {
                        Console.WriteLine("      [{0}] {1}", item.Status, item.Message);
                    }
                });
            }
        }

    }
}