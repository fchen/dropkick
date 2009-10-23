namespace dropkick.Configuration.Dsl.WinService
{
    using System;
    using Tasks.WinService;

    public class WinServiceTaskBuilder :
        WinServiceOptions
    {
        readonly PartCfg _part;
        readonly string _server;
        readonly string _serviceName;
        Action _action;

        public WinServiceTaskBuilder(ServerOptions options, string name)
        {
            _serviceName = name;
            _server = options.Name;
            _part = options.Part;
        }

        #region WinServiceOptions Members

        public WinServiceOptions Do(Action thingToDo)
        {
            _action = thingToDo;

            _part.AddTask(new WinServiceStopTask(_server, _serviceName));

            //child task
            _action();

            _part.AddTask(new WinServiceStartTask(_server, _serviceName));

            return this;
        }

        #endregion
    }
}