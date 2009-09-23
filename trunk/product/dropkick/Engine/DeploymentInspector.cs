namespace dropkick.Engine
{
    using Configuration.Dsl;
    using Execution;
    using Magnum.Reflection;

    public class DeploymentInspector : 
        ReflectiveVisitorBase<DeploymentInspector>,
        Configuration.Dsl.DeploymentInspector
    {
        public DeploymentInspector() :
            base("Inspect")
        {
        }

        public void Inspect(object obj)
        {
            base.Visit(obj);
        }

        public void Inspect(object obj, ExposeMoreInspectionSites additionalInspections)
        {
            base.Visit(obj, () =>
            {
                additionalInspections();
                return true;
            });
        }


        public bool Inspect(Deployment deployment)
        {
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        //TODO: This smells pretty nasty
        DeploymentPart _currentPart = null;
        public bool Inspect(Part part)
        {
            _currentPart = new DeploymentPart(part.Name);
            _plan.AddPart(_currentPart);
            return true;
        }

        public bool Inspect(Task task)
        {
            var detail = new DeploymentDetail(()=>task.Name, task.VerifyCanRun, task.Execute);
            _currentPart.AddDetail(detail);
            return true;
        }

        readonly DeploymentPlan _plan = new DeploymentPlan();

        public DeploymentPlan GetPlan()
        {
            return _plan;
        }
    }
}