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
        public static void ValidateNotNull<T>([ValidatedNotNull] T? value, string name)
            where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNullOrEmpty, name));
            }
        }

        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty?>> selector)
        {
            Contract.Assert(selector is not null);

            if ((selector!.Body is not MemberExpression { Expression: ParameterExpression } member))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ExpressionMustBeMemberExpression, selector), nameof(selector));
            }

            return member.Member.Name;
        }

        public static IReadOnlyCollection<string> GetPropertyPath<TSource, TProperty>(Expression<Func<TSource, TProperty?>> selector)
        {
            Contract.Assert(selector != null);
            Contract.Assert(selector!.NodeType == ExpressionType.Lambda);

            var result = new List<string>();

            Expression current = selector.Body;
            do
            {
                if (current is not MemberExpression member)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                        Resources.ExpressionMustBeMemberExpression, selector), nameof(selector));
                }

                result.Add(member.Member.Name);
                current = member.Expression!;
            } while (current is not ParameterExpression);

            result.Reverse();

            return result;
        }

        public static string? FormatName(IList<string> path, string? name)
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

        public static IValidatorContext<T> Perform<T>([ValidatedNotNull] this IValidatorContext<T> validator, Action action,
            bool skipNullValue = true)
        {
            ValidateNotNull(validator, nameof(validator));
            Contract.Assert(action is not null);

            if (validator.Exception is not null)
            {
                return validator;
            }

            if (skipNullValue && (validator.Value is null))
            {
                return validator;
            }

            action!();

            return validator;
        }
    }
}