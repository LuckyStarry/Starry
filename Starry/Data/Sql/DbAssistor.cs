using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Starry.Data.Sql.Builder;

namespace Starry.Data.Sql
{
    public abstract class DbAssistor
    {
        public DbAssistor()
            : this(DbMappingCollection.Default) { }

        public DbAssistor(DbMappingCollection dpMappings)
        {
            if (dpMappings == null)
            {
                throw new ArgumentNullException("dpMappings");
            }
            this.ParameterNameRecordFrom = "RecordFrom";
            this.ParameterNameRecordTo = "RecordTo";
            this.DbMappings = dpMappings;
        }

        public abstract string ParameterSymbol { get; }
        public string ParameterNameRecordFrom { set; get; }
        public string ParameterNameRecordTo { set; get; }
        public DbMappingCollection DbMappings { private set; get; }

        public DbCommandSource CreateDbCommandForGetPagedList(string selectText)
        {
            return this.CreateDbCommandForGetPagedList(selectText, null);
        }

        public abstract DbCommandSource CreateDbCommandForGetPagedList(string selectText, string order);

        public virtual DbCommandSource CreateDbCommandForAddEntity<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var mapping = this.DbMappings.GetDbMapping(typeof(TEntity));
            if (mapping == null)
            {
                throw new ArgumentException(string.Format("Cannot get the mapping of type {0}", typeof(TEntity).FullName), "TEntity");
            }

            var dbCommandSource = new DbCommandSource();
            var sqlText = new StringBuilder();
            sqlText.AppendLine("INSERT INTO {0}", mapping.TableName);
            var columns = new List<string>();
            foreach (var column in mapping.Columns)
            {
                var objVal = column.PropertyInfo.GetValue(entity, null);
                if (column.IngoreOnInsert)
                {
                    continue;
                }
                columns.Add(column.ColumnName);
                dbCommandSource.Parameters.Add(column.ColumnName, objVal);
            }
            sqlText.AppendLine("            ({0})", string.Join(", ", columns));
            sqlText.AppendLine("     VALUES ({0})", string.Join(", ", columns.Select(c => this.ParameterSymbol + c)));

            dbCommandSource.CommandText = sqlText.ToString();
            return dbCommandSource;
        }

        public abstract DbCommandSource CreateDbCommandForAddEntityAndGetRecordID<TEntity>(TEntity entity);

        protected DbCommandSource GetDbConditions(object conditions)
        {
            if (conditions == null)
            {
                return null;
            }
            if (conditions is string)
            {
                return new DbCommandSource { CommandText = (string)conditions };
            }

            var condition = default(Builder.Conditions.IDbCondition);
            foreach (var pCondition in conditions.GetType().GetProperties())
            {
                var subCondition = default(Builder.Conditions.IDbCondition);
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
                            var content = new Builder.Conditions.DbConditionContent();
                            content.ConditionString = string.Format("{0} = {2}{0}{1}", dbParamName, i, this.ParameterSymbol);
                            content.Parameters.Add(string.Format("{0}{1}", dbParamName, i), objArray.GetValue(i));
                            if (subCondition == null)
                            {
                                subCondition = content;
                            }
                            else
                            {
                                subCondition = subCondition.Or(content);
                            }
                        }
                    }
                    else
                    {
                        var content = new Builder.Conditions.DbConditionContent();
                        content.ConditionString = string.Format("{0} = {1}{0}", dbParamName, this.ParameterSymbol);
                        content.Parameters.Add(dbParamName, objVal);

                        subCondition = content;
                    }
                }
                if (subCondition != null)
                {
                    if (condition == null)
                    {
                        condition = subCondition;
                    }
                    else
                    {
                        condition = condition.And(subCondition);
                    }
                }
            }
            if (condition == null)
            {
                return null;
            }
            else
            {
                return condition.Generate();
            }
        }

        public virtual DbCommandSource CreateDbCommandForGetList<TEntity>(object conditions = null, object order = null)
        {
            var mapping = this.DbMappings.GetDbMapping(typeof(TEntity));
            if (mapping == null)
            {
                throw new ArgumentException(string.Format("Cannot get the mapping of type {0}", typeof(TEntity).FullName), "TEntity");
            }

            var sqlText = new StringBuilder();
            sqlText.AppendLine("SELECT {0}", string.Join(", ", mapping.Columns.Select(c => c.ColumnName)));
            sqlText.AppendLine("  FROM {0}", mapping.TableName);

            var dbCommandSource = new DbCommandSource();
            var dbCommandSourceForCondition = this.GetDbConditions(conditions);
            if (dbCommandSourceForCondition != null)
            {
                sqlText.AppendLine(" WHERE {0}", dbCommandSourceForCondition.CommandText);
                foreach (var param in dbCommandSourceForCondition.Parameters)
                {
                }
            }
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
                                    array.Add(string.Format("{0} = {2}{0}{1}", dbParamName, i, this.ParameterSymbol));
                                    dbCommandSource.Parameters.Add(string.Format("{0}{1}", dbParamName, i), objArray.GetValue(i));
                                }
                                if (array != null && array.Any())
                                {
                                    listConditions.Add(string.Join(" OR ", array));
                                }
                            }
                            else
                            {
                                listConditions.Add(string.Format("{0} = {1}{0}", dbParamName, this.ParameterSymbol));
                                dbCommandSource.Parameters.Add(dbParamName, objVal);
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
            dbCommandSource.CommandText = sqlText.ToString();
            return dbCommandSource;
        }
    }
}
