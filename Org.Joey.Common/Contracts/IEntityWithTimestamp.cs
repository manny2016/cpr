using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface IEntityWithTimestamp
    {
        long Timestamp { get; }
    }
}
