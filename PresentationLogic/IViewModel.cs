﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLogic
{
    public interface IViewModel
    {
        void Initialize(Action whenDone, object model);
    }
}
