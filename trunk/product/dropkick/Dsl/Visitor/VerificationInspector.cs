namespace dropkick.Dsl.Visitor
{
    using System;
    using Engine;
    using Magnum.Reflection;

    public class VerificationInspector :
        ReflectiveVisitorBase<VerificationInspector>,
        DeploymentInspector
    {
        public VerificationInspector() :
            base("Inspect")
        {
        }

        public void Inspect(object obj)
        {
            base.Visit(obj);
        }
        
        public void Inspect(object obj, Action additionalInspections)
        {
            base.Visit(obj, () =>
                {
                    additionalInspections();
                    return true;
                });
        }

        public bool Inspect(Deployment deployment)
        {
            Console.WriteLine("Verifying '{0}'", deployment.GetType().Name);
            return true;
        }

        public bool Inspect(Part part)
        {
            Console.WriteLine("  Part: {0}", part.Name);
            return true;
        }

        public bool Inspect(Task task)
        {
            //log this
            var r = task.VerifyCanRun();
            Console.WriteLine("    Task: {0}", task.Name);
            foreach(var result in r.Results)
            {
                Console.WriteLine("      [{0}]{1}", result.Status, result.Message);
            }

            return true;
        }
    }
}