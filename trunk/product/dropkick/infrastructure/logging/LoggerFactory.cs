using System;

namespace dropkick.infrastructure.logging
{
    public interface LoggerFactory
    {
        Logger create_logger_bound_to(Object type);
    }
}