using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public class JobTerminatedException : Exception
    {
        public override string Message
        {
            get
            {
                return "Job is terminated.";
            }
        }
    }
}
