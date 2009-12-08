namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using DeploymentModel;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly DeploymentServer _server;
        readonly string _serviceName;

        public WinServiceTaskBuilder(DeploymentServer server, string name)
        {
            _serviceName = name;
            _server = server;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action registerAdditionalActions)
        {
            _server.AddDetail(new WinServiceStopTask(_server.Name, _serviceName).ToDetail());

            //child task
            registerAdditionalActions();

            _server.AddDetail(new WinServiceStartTask(_server.Name, _serviceName).ToDetail());

            return this;
        }

        #endregion
    }
}