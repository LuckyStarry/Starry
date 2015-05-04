using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace Starry.Data.Assistant
{
    public abstract class CommandBuilder
    {
        protected CommandBuilder(object entity)
        {
        }

        public abstract string CreateSelectCommand();
        public string CreateSelectCommand(object parameters)
        {
            var @params = new Dictionary<string, object>();
            foreach (var propertyInfo in parameters.GetType().GetProperties())
            {
                @params.Add(propertyInfo.Name, propertyInfo.GetValue(parameters, null));
            }
            return this.CreateSelectCommand(@params);
        }

        public abstract string CreateSelectCommand(IDictionary<string, object> parameters);
    }

    public abstract class CommandBuilder<T> : CommandBuilder
        where T : class
    {
        protected CommandBuilder(T entity) : base(entity) { }
    }
}
