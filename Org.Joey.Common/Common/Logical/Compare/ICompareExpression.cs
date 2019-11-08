using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface ICompareExpression
    {
        bool Compare(object a, object b = null);
        ObjectChooseOptions Option { get; }
        string OutStr { get;  }
    }
}
