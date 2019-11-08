using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface IObjectCompare<A, B> : ICompareExpression
    {
        bool Compare(A a, B b);
    }
}
