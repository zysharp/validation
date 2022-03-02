using System;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace ZySharp.Validation
{
    /// <summary>
    /// Provides various methods to work with argument references.
    /// </summary>
    public static class ArgumentReference
    {
        /// <summary>
        /// Selects an argument reference.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        /// <returns>A new reference to the given argument.</returns>
        public static IArgumentReference<T> For<T>([ValidatedNotNull][NoEnumeration] T value, string name)
        {
            ValidationInternals.ValidateNotNull(name, nameof(name));

            return new ValidatorContext<T>(value, name);
        }

        /// <summary>
        /// Selects a property reference.
        /// <para>
        ///     The current argument reference must not contain a <c>null</c> value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TNext">The type of the selected property.</typeparam>
        /// <param name="reference">The current argument reference.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a <see cref="MemberExpression"/> (e.g. <c>x => x.Property.Sub</c>).
        ///     </para>
        /// </param>
        /// <returns>A new reference to the selected property.</returns>
        public static IArgumentReference<TNext> For<T, TNext>(this IArgumentReference<T> reference,
            Expression<Func<T, TNext>> selector)
        {
            ValidationInternals.ValidateNotNull(reference, nameof(reference));
            ValidationInternals.ValidateNotNull(selector, nameof(selector));

            if (reference.Value is null)
            {
                throw new ArgumentException(Resources.ReferenceMustNotContainNullValue, nameof(reference));
            }

            var path = ValidationInternals.GetPropertyPath(selector);
            var value = selector.Compile().Invoke(reference.Value);

            var result = new ValidatorContext<TNext>(value, reference.Path.Concat(path), null);

            return result;
        }
    }
}