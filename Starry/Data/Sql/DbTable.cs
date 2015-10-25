using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starry.Data.Sql
{
    public abstract class DbTable
    {
        public DbTable(DbContext dbContext, string tableName)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            this.DbContext = dbContext;
            this.TableName = tableName;
        }

        public DbContext DbContext { private set; get; }
        public string TableName { internal set; get; }
    }

    public class DbTable<TEntity> : DbTable
        where TEntity : new()
    {
        public DbTable(DbContext dbContext)
            : this(dbContext, typeof(TEntity).Name)
        {

        }

        public DbTable(DbContext dbContext, string tableName)
            : base(dbContext, tableName)
        {
            var dbColumnOnly = false;
            var tableAttrs = this.GetType().GetCustomAttributes(typeof(DbTableAttribute), true);
            if (tableAttrs != null && tableAttrs.Any())
            {
                var tableAttr = tableAttrs.First() as DbTableAttribute;
                if (!string.IsNullOrWhiteSpace(tableAttr.TableName))
                {
                    this.TableName = tableAttr.TableName.Trim();
                    dbColumnOnly = tableAttr.DbColumnOnly;
                }
            }

            var pkCount = 0;
            var columns = new List<DbColumn>();
            this.entityProperties = typeof(TEntity).GetProperties();
            foreach (var propertyInfo in this.entityProperties)
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    var isPK = false;
                    var columnName = propertyInfo.Name;
                    var attributes = propertyInfo.GetCustomAttributes(true);
                    if (attributes != null && attributes.Any())
                    {
                        if (attributes.Any(attr => attr is DbIgnoreAttribute))
                        {
                            continue;
                        }
                        if (attributes.Any(attr => attr is DbPrimaryKeyAttribute))
                        {
                            if (propertyInfo.PropertyType == typeof(Int16)
                                || propertyInfo.PropertyType == typeof(Int32)
                                || propertyInfo.PropertyType == typeof(Int64))
                            {
                                isPK = true;
                                pkCount++;
                            }
                            else
                            {
                                throw new ArgumentException(
                                    string.Format("The primary key must be short int or long"), "DbPrimaryKey");
                            }
                        }
                        var columnAttr = attributes.FirstOrDefault(attr => attr is DbColumnAttribute);
                        if (columnAttr == null && dbColumnOnly)
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
                    else if (dbColumnOnly)
                    {
                        continue;
                    }
                    columns.Add(new DbColumn { PropertyName = propertyInfo.Name, ColumnName = columnName, IsPrimaryKey = isPK });
                }
            }
            if (pkCount != 1)
            {
                throw new ArgumentException(
                    string.Format("The table must have only one primary key, table [{0}] has {1}", this.TableName, pkCount), "TEntity");
            }
            if (columns.Count == 0)
            {
                throw new ArgumentException(
                    string.Format("The table must have a valid column!"), "TEntity");
            }
            this.columns = columns.ToArray();
        }

        private readonly PropertyInfo[] entityProperties;
        private readonly DbColumn[] columns;
        public DbColumn[] Columns { get { return columns; } }

        public virtual IEnumerable<TEntity> GetList(object conditions = null, object sort = null)
        {
            var sqlText = new StringBuilder();
            sqlText.AppendLine("SELECT {0}", string.Join(", ", this.Columns.Select(c => c.ColumnName)));
            sqlText.AppendLine("  FROM {0}", this.TableName);

            var dbCommand = this.DbContext.DbEntity.CreateDbCommand();
            var paramPrefix = this.DbContext.DbEntity.ParameterPrefix;
            if (conditions != null)
            {
                if (conditions is string)
                {
                    sqlText.AppendLine(" WHERE {0}", conditions);
                }
                else
                {
                    var listConditions = new List<string>();
                    foreach (var pCondition in conditions.GetType().GetProperties())
                    {
                        if (pCondition.CanRead)
                        {
                            var dbParamName = pCondition.Name;
                            var objVal = pCondition.GetValue(conditions, null);
                            if (objVal is System.Array)
                            {
                                var array = new List<string>();
                                var objArray = objVal as System.Array;
                                for (var i = 0; i < objArray.Length; i++)
                                {
                                    array.Add(string.Format("{0} = {2}{0}{1}", dbParamName, i, paramPrefix));
                                    dbCommand.AppendParameter(string.Format("{0}{1}", dbParamName, i), objArray.GetValue(i));
                                }
                                if (array != null && array.Any())
                                {
                                    listConditions.Add(string.Join(" OR ", array));
                                }
                            }
                            else
                            {
                                listConditions.Add(string.Format("{0} = {1}{0}", dbParamName, paramPrefix));
                                dbCommand.AppendParameter(dbParamName, objVal);
                            }
                        }
                    }
                    if (listConditions != null && listConditions.Any())
                    {
                        sqlText.AppendLine(" WHERE {0}", string.Join(" AND ", listConditions));
                    }
                }
            }
            if (sort != null)
            {
                if (sort is string)
                {
                    sqlText.AppendLine(" ORDER BY {0}", sort);
                }
                else
                {
                    var listSort = new List<string>();
                    foreach (var pCondition in sort.GetType().GetProperties())
                    {
                        if (pCondition.CanRead && pCondition.PropertyType == typeof(string))
                        {
                            var dbParamName = pCondition.Name;
                            var objVal = pCondition.GetValue(sort, null);
                            var sortType = ((string)objVal) ?? string.Empty;
                            switch (sortType.ToLower())
                            {
                                case "asc":
                                case "desc":
                                    listSort.Add(string.Format("{0} {1}", dbParamName, sortType));
                                    break;
                            }
                        }
                    }
                    if (listSort != null && listSort.Any())
                    {
                        sqlText.AppendLine(" ORDER BY {0}", string.Join(" ", listSort));
                    }
                }
            }
            dbCommand.CommandText = sqlText.ToString();
            var dataTable = this.DbContext.ExecuteDataTable(dbCommand);
            return dataTable.ToList<TEntity>();
        }

        public virtual int AddEntity(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var paramPrefix = this.DbContext.DbEntity.ParameterPrefix;
            var dbCommand = this.DbContext.DbEntity.CreateDbCommand();
            var sqlText = new StringBuilder();
            sqlText.AppendLine("INSERT INTO {0}", this.TableName);
            var columns = new List<string>();
            foreach (var column in this.Columns)
            {
                var objVal = this.entityProperties.First(p => p.Name == column.PropertyName).GetValue(entity, null);
                if (column.IsPrimaryKey)
                {
                    continue;
                }
                columns.Add(column.ColumnName);
                dbCommand.AppendParameter(string.Format("{0}{1}", this.DbContext.DbEntity.ParameterPrefix, column.ColumnName), objVal);
            }
            sqlText.AppendLine("            ({0})", string.Join(", ", columns));
            sqlText.AppendLine("     VALUES ({0})", string.Join(", ", columns.Select(c => paramPrefix + c)));

            dbCommand.CommandText = sqlText.ToString();
            return this.DbContext.ExecuteNonQuery(dbCommand);
        }
    }
}
