namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly Role _role;
        readonly Server _server;
        readonly string _serviceName;

        public WinServiceTaskBuilder(Server server, string name)
        {
            _serviceName = name;
            _server = server;
            _role = server.Role;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action registerAdditionalActions)
        {
            _role.AddTask(new WinServiceStopTask(_server.Name, _serviceName));

            //child task
            registerAdditionalActions();

            _role.AddTask(new WinServiceStartTask(_server.Name, _serviceName));

            return this;
        }

        #endregion
    }
}