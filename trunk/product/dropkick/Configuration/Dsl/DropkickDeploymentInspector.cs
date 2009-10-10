namespace dropkick.Configuration.Dsl
{
    using System;
    using DeploymentModel;
    using Engine;
    using Magnum.Reflection;

    public class DropkickDeploymentInspector :
        ReflectiveVisitorBase<DropkickDeploymentInspector>,
        DeploymentInspector
    {
        readonly DeploymentPlan _plan = new DeploymentPlan();
        DeploymentPart _currentPart;
        Func<DeploymentPart, bool> _partCriteria;

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

        #region Inspect Methods
        public bool Inspect(Deployment deployment)
        {
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        //TODO: This smells pretty nasty

        public bool Inspect(Part part)
        {
            _currentPart = new DeploymentPart(part.Name);
            if(_partCriteria(_currentPart))
                _plan.AddPart(_currentPart);

            return true;
        }

        public bool Inspect(Task task)
        {
            var detail = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
            _currentPart.AddDetail(detail);
            return true;
        }
        #endregion

        public DeploymentPlan GetPlan(Deployment deployment, DeploymentArguments args)
        {
            _partCriteria = Criteria(args);
            Inspect(deployment);
            return _plan;
        }

        static Func<DeploymentPart, bool> Criteria(DeploymentArguments args)
        {
            Func<DeploymentPart, bool> criteria = p => true;

            //need multi-part deploys too
            if (!args.Part.Equals("ALL"))
                criteria = p => p.Name.Equals(args.Part);

            return criteria;
        }
    }
}