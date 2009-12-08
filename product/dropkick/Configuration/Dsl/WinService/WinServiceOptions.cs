namespace dropkick.Configuration.Dsl.WinService
{
    using System;

    public interface WinServiceOptions
    {
        WinServiceOptions Do(Action registerAdditionalActions);
    }
}