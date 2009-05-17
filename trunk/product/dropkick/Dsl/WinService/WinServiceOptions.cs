namespace dropkick.Dsl.WinService
{
    using System;

    public interface WinServiceOptions
    {
        WinServiceOptions Verify();
        WinServiceOptions Do(Action thingToDo);
    }
}