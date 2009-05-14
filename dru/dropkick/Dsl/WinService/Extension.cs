namespace dropkick.Dsl.WinService
{
    using System;

    public static class Extension
    {
        public static WinServiceOptions WinService(this ServerOptions server, string serviceName)
        {
            return null;
        }
    }

    public interface WinServiceOptions
    {
        WinServiceOptions Verify();
        WinServiceOptions Do(Action thingToDo);
    }
}