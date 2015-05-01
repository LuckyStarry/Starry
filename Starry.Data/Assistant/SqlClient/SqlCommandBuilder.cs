using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlCommandBuilder : ISqlCommandBuilder<System.Data.SqlClient.SqlCommand>
    {
        public System.Data.SqlClient.SqlCommand CreateSelectCommand<T>()
        {
            throw new NotImplementedException();
        }

        public System.Data.SqlClient.SqlCommand CreateInsertCommand<T>()
        {
            throw new NotImplementedException();
        }

        public System.Data.SqlClient.SqlCommand CreateUpdateCommand<T>()
        {
            throw new NotImplementedException();
        }

        public System.Data.SqlClient.SqlCommand CreateDeleteCommand<T>()
        {
            throw new NotImplementedException();
        }

        IDbCommand ISqlCommandBuilder.CreateSelectCommand<T>()
        {
            return this.CreateSelectCommand<T>();
        }

        IDbCommand ISqlCommandBuilder.CreateInsertCommand<T>()
        {
            return this.CreateInsertCommand<T>();
        }

        IDbCommand ISqlCommandBuilder.CreateUpdateCommand<T>()
        {
            return this.CreateUpdateCommand<T>();
        }

        IDbCommand ISqlCommandBuilder.CreateDeleteCommand<T>()
        {
            return this.CreateDeleteCommand<T>();
        }
    }
}
