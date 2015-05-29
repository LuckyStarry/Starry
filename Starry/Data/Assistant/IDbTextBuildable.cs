using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Assistant
{
    public interface IDbTextBuildable
    {
        string CreateSelectCommandText();
        string CreateInsertCommandText();
        string CreateUpdateCommandText();
        string CreateDeleteCommandText();
    }
}
