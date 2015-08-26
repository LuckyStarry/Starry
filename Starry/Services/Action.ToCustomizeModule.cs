﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Starry.Services
{
    public static partial class ActionExtend
    {
        public static CustomizeModule ToCustomizeModule(this Action<CancellationToken> action, Core.IService service)
        {
            return new CustomizeModuleLightly(action, service);
        }
    }
}
