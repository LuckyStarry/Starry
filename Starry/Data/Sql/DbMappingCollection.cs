using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public class DbMappingCollection
    {
        private IDictionary<string, DbMapping> mappings = new Dictionary<string, DbMapping>();

        public static DbMappingCollection Default { get { return DbMappingCollectionDefault.Instance; } }

        public DbMapping GetDbMapping(Type type)
        {
            var typeName = type.FullName;
            if (!this.mappings.ContainsKey(typeName))
            {
                lock (this.mappings)
                {
                    if (!this.mappings.ContainsKey(typeName))
                    {
                        var mapping = this.GenerateMapping(type);
                        if (mapping != null)
                        {
                            this.Register(type, mapping);
                        }
                        return null;
                    }
                }
            }
            return this.mappings[typeName];
        }

        public virtual DbMapping GenerateMapping(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            var mapping = new DbMapping();
            mapping.TableName = type.Name;
            mapping.DbColumnOnly = false;
            var tableAttrs = type.GetCustomAttributes(typeof(DbTableAttribute), true);
            if (tableAttrs != null && tableAttrs.Any())
            {
                var tableAttr = tableAttrs.First() as DbTableAttribute;
                if (!string.IsNullOrWhiteSpace(tableAttr.TableName))
                {
                    mapping.TableName = tableAttr.TableName.Trim();
                    mapping.DbColumnOnly = tableAttr.DbColumnOnly;
                }
            }

            var columns = new List<DbColumn>();
            var entityProperties = type.GetProperties();
            foreach (var propertyInfo in entityProperties)
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var isPK = false;
                    var ignoreOnInsert = false;
                    var columnName = propertyInfo.Name;
                    var attributes = propertyInfo.GetCustomAttributes(true);
                    if (attributes != null && attributes.Any())
                    {
                        if (attributes.Any(attr => attr is DbIgnoreAttribute))
                        {
                            continue;
                        }
                        var pkAttr = attributes.FirstOrDefault(attr => attr is DbPrimaryKeyAttribute);
                        if (pkAttr != null)
                        {
                            var attr = pkAttr as DbPrimaryKeyAttribute;
                            if (!string.IsNullOrWhiteSpace(attr.ColumnName))
                            {
                                columnName = attr.ColumnName.Trim();
                            }
                            ignoreOnInsert = attr.IngoreOnInsert;
                            isPK = true;
                        }
                        else
                        {
                            var columnAttr = attributes.FirstOrDefault(attr => attr is DbColumnAttribute);
                            if (columnAttr == null && mapping.DbColumnOnly)
                            {
                                continue;
                            }
                            if (columnAttr != null)
                            {
                                var attr = columnAttr as DbColumnAttribute;
                                if (!string.IsNullOrWhiteSpace(attr.ColumnName))
                                {
                                    columnName = attr.ColumnName.Trim();
                                }
                            }
                        }
                    }
                    else if (mapping.DbColumnOnly)
                    {
                        continue;
                    }
                    columns.Add(new DbColumn { PropertyInfo = propertyInfo, ColumnName = columnName, IsPrimaryKey = isPK, IngoreOnInsert = ignoreOnInsert });
                }
            }
            if (columns.Count == 0)
            {
                throw new ArgumentException(
                    string.Format("The table must have a valid column!"), "TEntity");
            }
            mapping.Columns = columns.ToArray();
            return mapping;
        }

        public void Register(Type type, DbMapping dbMapping)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (dbMapping == null)
            {
                throw new ArgumentNullException("dbMapping");
            }
            var typeName = type.FullName;
            lock (this.mappings)
            {
                if (this.mappings.ContainsKey(typeName))
                {
                    this.mappings[typeName] = dbMapping;
                }
                else
                {
                    this.mappings.Add(typeName, dbMapping);
                }
            }
        }
    }
}
