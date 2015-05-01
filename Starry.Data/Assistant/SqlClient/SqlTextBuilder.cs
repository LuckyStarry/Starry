using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlTextBuilder : IDbTextBuildable
    {
        public string CreateSelectCommand<T>()
        {
            throw new NotImplementedException();
        }

        public string CreateInsertCommand<T>()
        {
            throw new NotImplementedException();
        }

        public string CreateUpdateCommand<T>()
        {
            throw new NotImplementedException();
        }

        public string CreateDeleteCommand<T>()
        {
            throw new NotImplementedException();
        }
    }
}
