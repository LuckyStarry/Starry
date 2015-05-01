using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlCommandBuilder : IDbCommandBuilder<System.Data.SqlClient.SqlCommand>
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

        IDbCommand IDbCommandBuilder.CreateSelectCommand<T>()
        {
            return this.CreateSelectCommand<T>();
        }

        IDbCommand IDbCommandBuilder.CreateInsertCommand<T>()
        {
            return this.CreateInsertCommand<T>();
        }

        IDbCommand IDbCommandBuilder.CreateUpdateCommand<T>()
        {
            return this.CreateUpdateCommand<T>();
        }

        IDbCommand IDbCommandBuilder.CreateDeleteCommand<T>()
        {
            return this.CreateDeleteCommand<T>();
        }
    }
}
