using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Starry.Data.Assistant
{
    public interface ISqlCommandBuilder
    {
        IDbCommand CreateSelectCommand<T>();
        IDbCommand CreateInsertCommand<T>();
        IDbCommand CreateUpdateCommand<T>();
        IDbCommand CreateDeleteCommand<T>();
    }

    public interface ISqlCommandBuilder<TCommand> : ISqlCommandBuilder
        where TCommand : IDbCommand
    {
        TCommand CreateSelectCommand<T>();
        TCommand CreateInsertCommand<T>();
        TCommand CreateUpdateCommand<T>();
        TCommand CreateDeleteCommand<T>();
    }
}
