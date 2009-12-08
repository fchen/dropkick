namespace dropkick.Configuration.Dsl
{
    using System;
    using DeploymentModel;
    using Magnum.Collections;
    using Magnum.Reflection;

    public class DropkickDeploymentInspector :
        ReflectiveVisitorBase<DropkickDeploymentInspector>,
        DeploymentInspector
    {
        readonly DeploymentPlan _plan = new DeploymentPlan();
        DeploymentRole _currentRole;
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
            //TODO: handle the multiple servers here?
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        //TODO: This smells pretty nasty

        public bool Look(Role role)
        {
            _currentRole = new DeploymentRole(role.Name);
            foreach (var server in _serverMappings[role.Name])
            {
                _currentRole.AddServer(new DeploymentServer(server));
            }
            if(_roleCriteria(_currentRole))
                _plan.AddPart(_currentRole);

            return true;
        }

        public bool Look(Task task)
        {
            var detail = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
            _currentRole.ForEachServer(s=>s.AddDetail(detail));
            return true;
        }
        #endregion

        MultiDictionary<string, string> _serverMappings;
        public DeploymentPlan GetPlan(Deployment deployment, RoleCriteria criteria, MultiDictionary<string, string> serverMappings)
        {
            _roleCriteria = criteria;
            _serverMappings = serverMappings;
            //TODO: separate out?
            deployment.Inspect(this);
            return _plan;
        }
        
    }

    public delegate bool RoleCriteria(DeploymentRole role);
}