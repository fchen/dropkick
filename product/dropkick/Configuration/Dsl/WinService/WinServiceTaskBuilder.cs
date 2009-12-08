namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly RoleCfg _role;
        readonly string _server;
        readonly string _serviceName;
        Action _action;

        public WinServiceTaskBuilder(ServerOptions options, string name)
        {
            _serviceName = name;
            _server = options.Name;
            _role = options.Role;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action thingToDo)
        {
            _action = thingToDo;

            _role.AddTask(new WinServiceStopTask(_server, _serviceName));

            //child task
            _action();

            _role.AddTask(new WinServiceStartTask(_server, _serviceName));

            return this;
        }

        #endregion
    }
}