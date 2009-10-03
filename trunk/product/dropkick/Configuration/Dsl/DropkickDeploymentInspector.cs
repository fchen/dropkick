namespace dropkick.Configuration.Dsl
{
    using DeploymentModel;
    using Magnum.Reflection;

    public class DropkickDeploymentInspector :
        ReflectiveVisitorBase<DropkickDeploymentInspector>,
        DeploymentInspector
    {
        readonly DeploymentPlan _plan = new DeploymentPlan();
        DeploymentPart _currentPart;

        public DropkickDeploymentInspector() :
            base("Inspect")
        {
        }

        #region DeploymentInspector Members

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

        #endregion

        public bool Inspect(Deployment deployment)
        {
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        //TODO: This smells pretty nasty

        public bool Inspect(Part part)
        {
            _currentPart = new DeploymentPart(part.Name);
            _plan.AddPart(_currentPart);
            return true;
        }

        public bool Inspect(Task task)
        {
            var detail = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
            _currentPart.AddDetail(detail);
            return true;
        }

        public DeploymentPlan GetPlan()
        {
            return _plan;
        }
    }
}