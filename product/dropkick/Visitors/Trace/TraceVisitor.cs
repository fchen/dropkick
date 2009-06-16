namespace dropkick.Dsl.Visitor
{
    using System;
    using Magnum.Reflection;

    public class TraceVisitor :
        ReflectiveVisitorBase<TraceVisitor>,
        DeploymentInspector
    {
        public TraceVisitor() :
            base("Inspect")
        {
        }

        public void Inspect(object obj)
        {
            base.Visit(obj);
        }

        public bool Inspect(Deployment deployment)
        {
            Console.WriteLine("Deployment:{0}", deployment.GetType().Name);
            return true;
        }

        public bool Inspect(Part part)
        {
            Console.WriteLine("    Part:{0}", part.Name);
            return true;
        }

        public bool Inspect(Task task)
        {
            Console.WriteLine("        Task:{0}", task.Name);
            return true;
        }

        public void Inspect(object obj, ExposeMoreInspectionSites additionalInspections)
        {
            base.Visit(obj, () =>
                {
                    additionalInspections();
                    return true;
                });
        }
    }
}