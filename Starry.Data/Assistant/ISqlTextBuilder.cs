using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant
{
    public interface ISqlTextBuilder
    {
        string CreateSelectCommand<T>();
        string CreateInsertCommand<T>();
        string CreateUpdateCommand<T>();
        string CreateDeleteCommand<T>();
    }
}
