using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Starry.Data.Sql
{
    public abstract class DbCommandGenerator
    {
        public DbCommandGenerator()
        {
            this.ParameterNameNumFrom = "numfrom";
            this.ParameterNameNumTo = "numto";
        }

        public abstract string ParameterNamePrefix { get; }
        public string ParameterNameNumFrom { set; get; }
        public string ParameterNameNumTo { set; get; }
        public DbEntity DbEntity { internal set; get; }

        public DbCommand CreateDbCommandForGetPagedList(string selectText)
        {
            return this.CreateDbCommandForGetPagedList(selectText, null);
        }

        public abstract DbCommand CreateDbCommandForGetPagedList(string selectText, string order);

        public virtual DbCommand CreateDbCommandForAddEntity<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var mapping = DbMappingCollection.Default.GetDbMapping(typeof(TEntity));
            if (mapping == null)
            {
                throw new ArgumentException(string.Format("Cannot get the mapping of type {0}", typeof(TEntity).FullName), "TEntity");
            }

            var dbCommand = this.DbEntity.CreateDbCommand();
            var sqlText = new StringBuilder();
            sqlText.AppendLine("INSERT INTO {0}", mapping.TableName);
            var columns = new List<string>();
            foreach (var column in mapping.Columns)
            {
                var objVal = column.PropertyInfo.GetValue(entity, null);
                if (column.IsPrimaryKey)
                {
                    continue;
                }
                columns.Add(column.ColumnName);
                dbCommand.AppendParameter(string.Format("{0}{1}", this.ParameterNamePrefix, column.ColumnName), objVal);
            }
            sqlText.AppendLine("            ({0})", string.Join(", ", columns));
            sqlText.AppendLine("     VALUES ({0})", string.Join(", ", columns.Select(c => this.ParameterNamePrefix + c)));

            dbCommand.CommandText = sqlText.ToString();
            return dbCommand;
        }

        public virtual DbCommand CreateDbCommandForGetList<TEntity>(object conditions = null, object order = null)
        {
            var mapping = DbMappingCollection.Default.GetDbMapping(typeof(TEntity));
            if (mapping == null)
            {
                throw new ArgumentException(string.Format("Cannot get the mapping of type {0}", typeof(TEntity).FullName), "TEntity");
            }

            var sqlText = new StringBuilder();
            sqlText.AppendLine("SELECT {0}", string.Join(", ", mapping.Columns.Select(c => c.ColumnName)));
            sqlText.AppendLine("  FROM {0}", mapping.TableName);

            var dbCommand = this.DbEntity.CreateDbCommand();
            var paramPrefix = this.ParameterNamePrefix;
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
            if (order != null)
            {
                if (order is string)
                {
                    sqlText.AppendLine(" ORDER BY {0}", order);
                }
                else
                {
                    var listSort = new List<string>();
                    foreach (var pCondition in order.GetType().GetProperties())
                    {
                        if (pCondition.CanRead && pCondition.PropertyType == typeof(string))
                        {
                            var dbParamName = pCondition.Name;
                            var objVal = pCondition.GetValue(order, null);
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
            return dbCommand;
        }
    }
}
