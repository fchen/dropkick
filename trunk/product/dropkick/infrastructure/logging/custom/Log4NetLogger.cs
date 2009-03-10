namespace dropkick.infrastructure.logging.custom
{
    using log4net;

    public class Log4NetLogger : Logger
    {
        private readonly ILog logger;

        public Log4NetLogger(ILog logger)
        {
            this.logger = logger;
            logger.DebugFormat("Initializing {0}<{1}>", GetType().FullName, logger.Logger.Name);
        }

        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void DebugFormat(string message, params object[] args)
        {
            logger.DebugFormat(message, args);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void InfoFormat(string message, params object[] args)
        {
            logger.InfoFormat(message, args);
        }

        public void Warn(object message)
        {
            logger.Warn(message);
        }

        public void WarnFormat(string message, params object[] args)
        {
            logger.WarnFormat(message, args);
        }

        public void Error(object message)
        {
            logger.Error(message);
        }

        public void ErrorFormat(string message, params object[] args)
        {
            logger.ErrorFormat(message, args);
        }

        public void Fatal(object message)
        {
            logger.Fatal(message);
        }

        public void FatalFormat(string message, params object[] args)
        {
            logger.FatalFormat(message, args);
        }
    }
}