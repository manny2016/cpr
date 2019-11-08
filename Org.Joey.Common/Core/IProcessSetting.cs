﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Joey.Common
{
    public interface IProcessSetting<ProcessingResult>
    {
        IProcessService<ProcessingResult> GenerateProcessService();
    }
}
