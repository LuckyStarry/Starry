using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql.Builder.Commands
{
    public class InsertCommandBuilder : CommandBuilder
    {
        public sealed override string CommandText
        {
            get { return "INSERT"; }
        }
    }
}
