using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant
{
    public interface IDbCommandBuildable
    {
        public string[] GetDbColumns();
    }
}
