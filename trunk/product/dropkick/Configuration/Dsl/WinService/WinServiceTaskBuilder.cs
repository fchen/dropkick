namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly Role _role;
        readonly string _serviceName;
        Action _action;

        public WinServiceTaskBuilder(Role role, string name)
        {
            _serviceName = name;
            _role = role;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action thingToDo)
        {
            _action = thingToDo;

            _role.AddTask(new WinServiceStopTask(_role, _serviceName));

            //child task
            _action();

            _role.AddTask(new WinServiceStartTask(_role, _serviceName));

            return this;
        }

        #endregion
    }
}