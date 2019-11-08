using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface IProcessingResultService<ProcessingResult> : IDisposable
    {
        void Save(IEnumerable<ProcessingResult> results);
    }
}
