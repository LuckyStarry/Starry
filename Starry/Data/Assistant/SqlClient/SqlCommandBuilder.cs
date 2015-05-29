using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Starry.Data.Assistant.SqlClient
{
    public class SqlCommandBuilder
    {
        public SqlCommandBuilder()
        {
            this.SqlParameters = new List<SqlParameter>();
        }

        public string BuildSqlString(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                return this.BuildSqlString((expression as LambdaExpression).Body);
            }
            if (expression is BinaryExpression)
            {
                var expr = expression as BinaryExpression;
                var calc = string.Empty;
                switch (expr.NodeType)
                {
                    case ExpressionType.And:
                    case ExpressionType.AndAlso: calc = "AND"; break;
                    case ExpressionType.Or:
                    case ExpressionType.OrElse: calc = "OR"; break;
                    case ExpressionType.Add: calc = "+"; break;
                    case ExpressionType.Subtract: calc = "-"; break;
                    case ExpressionType.Multiply: calc = "*"; break;
                    case ExpressionType.Divide: calc = "/"; break;
                    case ExpressionType.LessThan: calc = "<"; break;
                    case ExpressionType.LessThanOrEqual: calc = "<="; break;
                    case ExpressionType.Equal: calc = "="; break;
                    case ExpressionType.NotEqual: calc = "<>"; break;
                    case ExpressionType.GreaterThanOrEqual: calc = ">="; break;
                    case ExpressionType.GreaterThan: calc = ">"; break;
                }
                return string.Format("({0} {1} {2})", this.BuildSqlString(expr.Left), calc, this.BuildSqlString(expr.Right));
            }
            else if (expression is ConstantExpression)
            {
                var expr = expression as ConstantExpression;
                var pName = "@p" + this.SqlParameters.Count.ToString();
                this.SqlParameters.Add(new SqlParameter(pName, expr.Value));
                return pName;
            }
            else if (expression is UnaryExpression)
            {
                var expr = expression as UnaryExpression;
                switch (expr.NodeType)
                {
                    case ExpressionType.Not: return string.Format("NOT ({0})", this.BuildSqlString(expr.Operand));
                    default: return string.Format("({0})", this.BuildSqlString(expr.Operand));
                }
            }
            else if (expression is MemberExpression)
            {
                var expr = expression as MemberExpression;
                var member = expr.Member.MemberType;
                return expr.Member.Name;
            }
            throw new NotImplementedException();
        }

        private List<SqlParameter> SqlParameters;
    }
}
