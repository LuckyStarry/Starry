using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Commands
{
    public abstract class CommandBuilder
    {
        public abstract string CommandText { get; }
    }
}
