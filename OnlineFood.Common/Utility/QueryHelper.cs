using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Common.Utility
{
    internal static class QueryHelper
    {
        public static bool PropertyExists<T>(string propertyName)
        {
            return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
        }

        public static Expression<Func<T, string>> GetPropertyExpression<T>(string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            var paramterExpression = Expression.Parameter(typeof(T));

            return (Expression<Func<T, string>>)Expression.Lambda(Expression.PropertyOrField(paramterExpression, propertyName), paramterExpression);
        }

        public static Expression<Func<T, int>> GetPropertyExpressionInt<T>(string propertyName)
        {
            if (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) == null)
            {
                return null;
            }

            var paramterExpression = Expression.Parameter(typeof(T));

            return (Expression<Func<T, int>>)Expression.Lambda(Expression.PropertyOrField(paramterExpression, propertyName), paramterExpression);
        }

        public static System.Type Type(this MemberExpression memberExpression)
        {
            if (memberExpression == null)
            {
                return null;
            }
            MemberInfo member = memberExpression.Member;
            if (member.MemberType == MemberTypes.Property)
            {
                return ((PropertyInfo)member).PropertyType;
            }
            if (member.MemberType != MemberTypes.Field)
            {
                throw new NotSupportedException();
            }
            return ((FieldInfo)member).FieldType;
        }

        public static MemberExpression ToMemberExpression(this LambdaExpression expression)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body == null)
            {
                UnaryExpression expression3 = expression.Body as UnaryExpression;
                if (expression3 != null)
                {
                    body = expression3.Operand as MemberExpression;
                }
            }
            return body;
        }

        public static string MemberWithoutInstance(this LambdaExpression expression)
        {
            return ExpressionHelper.GetExpressionText(expression);
        }
    }
}
