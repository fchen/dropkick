namespace dropkick.infrastructure.logging
{
    public interface Logger
    {
        void Debug(object message);
        void DebugFormat(string message, params object[] args);
        void Info(object message);
        void InfoFormat(string message, params object[] args);
        void Warn(object message);
        void WarnFormat(string message, params object[] args);
        void Error(object message);
        void ErrorFormat(string message, params object[] args);
        void Fatal(object message);
        void FatalFormat(string message, params object[] args);
    }
}