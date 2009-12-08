namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly Role _role;
        readonly string _server;
        readonly string _serviceName;
        Action _action;

        public WinServiceTaskBuilder(Server server, string name)
        {
            _serviceName = name;
            _server = server.Name;
            _role = server.Role;
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