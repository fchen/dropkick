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
        RoleCriteria _roleCriteria;

        public DropkickDeploymentInspector() :
            base("Look")
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

        #region Inspect Methods
        public bool Look(Deployment deployment)
        {
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        //TODO: This smells pretty nasty

        public bool Look(Role role)
        {
            _currentPart = new DeploymentPart(role.Name);
            if(_roleCriteria(_currentPart))
                _plan.AddPart(_currentPart);

            return true;
        }

        public bool Look(Task task)
        {
            var detail = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
            _currentPart.AddDetail(detail);
            return true;
        }
        #endregion

        public DeploymentPlan GetPlan(Deployment deployment, RoleCriteria criteria)
        {
            _roleCriteria = criteria;

            //TODO: separate out?
            deployment.Inspect(this);
            return _plan;
        }
        
    }

    public delegate bool RoleCriteria(DeploymentPart part);
}