using System;

namespace dropkick.infrastructure.logging
{
    public interface LogFactory
    {
        Logger create_logger_bound_to(Object type);
    }
}