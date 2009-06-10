namespace dropkick.Dsl.WinService
{
    using System;

    public class WinServiceTaskBuilder : 
        WinServiceOptions
    {
        private string _serviceName;
        private string _server;
        private Action _action;
        private Part _part;

        public WinServiceTaskBuilder(ServerOptions options, string name)
        {
            _serviceName = name;
            _server = options.Name;
            _part = options.Part;
        }

        public WinServiceOptions Do(Action thingToDo)
        {
            _action = thingToDo;
            
            _part.AddTask(new WinServiceStopTask(_server, _serviceName));
            //child task
            _part.AddTask(new WinServiceStartTask(_server, _serviceName));

            return this;
        }
    }
}