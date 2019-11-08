using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface IWorkItem
    {
        object WorkItemState { get; }

        void Execute();

        void Abort();
    }
}
