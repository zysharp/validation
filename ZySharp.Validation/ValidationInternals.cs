using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace ZySharp.Validation
{
    internal static class ValidationInternals
    {
        public static void ValidateNotNull<T>([ValidatedNotNull] T value, string name)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNullOrEmpty, name));
            }
        }

        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> selector)
        {
            Contract.Assert(selector != null);

            if ((selector.NodeType != ExpressionType.Lambda) ||
                (selector.Body is not MemberExpression member) ||
                (member.Expression.NodeType != ExpressionType.Parameter))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ExpressionMustBeMemberExpression, selector), nameof(selector));
            }

            return member.Member.Name;
        }

        public static string FormatName(IList<string> path, string name)
        {
            Contract.Assert(path != null);

            if (!path.Any())
            {
                return name;
            }

            var items = (name is null)
                ? path
                : path.Concat(new[] { name });

            return string.Join(".", items);
        }
    }
}