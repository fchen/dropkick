namespace dropkick.Configuration.Dsl
{
    using DeploymentModel;
    using Engine;
    using Magnum.Reflection;

    public class DropkickDeploymentInspector :
        ReflectiveVisitorBase<DropkickDeploymentInspector>,
        DeploymentInspector
    {
        readonly DeploymentPlan _plan = new DeploymentPlan();
        DeploymentRole _currentRole; //TODO: seems hackish
        RoleToServerMap _serverMappings;

        public DropkickDeploymentInspector() :
            base("Look")
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

        #region Inspect Methods
        public bool Look(Deployment deployment)
        {
            _plan.Name = deployment.GetType().Name;
            return true;
        }

        public bool Look(Role role)
        {
            _currentRole = _plan.AddRole(role.Name);

            foreach (var serverName in _serverMappings.GetServers(role.Name))
            {
                _currentRole.AddServer(serverName);
            }
            
            return true;
        }

        public bool Look(Server server)
        {
            //TODO: implement
            return true;
        }

        public bool Look(TaskBuilder taskBuilder)
        {
            //TODO: hackish
            _currentRole.ForEachServer(server =>
            {
                var task = taskBuilder.ConstructTasksForServer(server);
                var detail = new DeploymentDetail(() => task.Name, task.VerifyCanRun, task.Execute);
                server.AddDetail(detail);
            });

            return true;
        }
        #endregion

        public DeploymentPlan GetPlan(Deployment deployment, RoleToServerMap serverMappings)
        {
            _serverMappings = serverMappings;
            
            deployment.InspectWith(this);
            
            return _plan;
        }
        
    }
}