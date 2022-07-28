using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        /// <summary>
        /// Selects a property for validation.
        /// <para>
        ///     All validations for the selected property will be skipped if the current validator context contains
        ///     a <c>null</c> value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TNext">The type of the selected property.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a <see cref="MemberExpression"/> (e.g. <c>x => x.Property</c>).
        ///     </para>
        /// </param>
        /// <param name="action">The action to perform for the selected property.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T?> For<T, TNext>(this IValidatorContext<T?> validator,
            Expression<Func<T, TNext?>> selector, Action<IValidatorContext<TNext?>> action)
        {
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            return validator.Perform(() =>
            {
                var name = ValidationInternals.GetPropertyName(selector);
                var value = selector.Compile().Invoke(validator.Value!);

                var context = new ValidatorContext<TNext?>(value, validator.Path, name);
                action.Invoke(context);

                validator.SetForeignException(context);
            });
        }

        /// <summary>
        /// Selects all elements of a <see cref="IEnumerable{T}"/> property for validation.
        /// <para>
        ///     All validations for the selected property will be skipped if the current validator context contains
        ///     a <c>null</c> value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TItem">The type of an individual item in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a <see cref="MemberExpression"/> (e.g. <c>x => x.Property</c>) or
        ///         a <see cref="ParameterExpression"/> to select all elements of the current
        ///         value (e.g. <c>x => x</c>).
        ///     </para>
        /// </param>
        /// <param name="action">The action to perform for each item of the <see cref="IEnumerable{T}"/>.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T?> ForEach<T, TItem>(this IValidatorContext<T?> validator,
            Expression<Func<T, IEnumerable<TItem?>?>> selector, Action<IValidatorContext<TItem?>> action)
        {
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            return validator.Perform(() =>
            {
                var name = (selector.Body is ParameterExpression)
                    ? null
                    : ValidationInternals.GetPropertyName(selector);
                var value = selector.Compile().Invoke(validator.Value!);

                if (value is null)
                {
                    return;
                }

                int i = 0;
                foreach (var item in value)
                {
                    var context = (name is null)
                        ? new ValidatorContext<TItem?>(item, $"{ValidationInternals.FormatName(validator.Path, null)}[{i++}]")
                        : new ValidatorContext<TItem?>(item, validator.Path, $"{name}[{i++}]");

                    action.Invoke(context);

                    if (context.Exception is not null)
                    {
                        validator.SetForeignException(context);
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Performs validations if a user-defined condition is met.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="predicate">The filter predicate.</param>
        /// <param name="action">The action to perform if the condition is met.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T?> When<T>(this IValidatorContext<T?> validator, Func<T?, bool> predicate,
            Action<IValidatorContext<T?>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(predicate, nameof(predicate));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            if (validator.Exception is not null)
            {
                return validator;
            }

            if (predicate(validator.Value))
            {
                action.Invoke(validator);
            }

            return validator;
        }

        /// <summary>
        /// Performs validations after projecting the current value to a new type.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TNew">The type of the projected value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">The selector lambda.</param>
        /// <param name="action">The action to perform with the projected value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T?> Select<T, TNew>(this IValidatorContext<T> validator, Func<T?, TNew?> selector,
            Action<IValidatorContext<TNew?>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            if (validator.Exception is not null)
            {
                return validator;
            }

            var context = new ValidatorContext<TNew?>(selector.Invoke(validator.Value), validator.Path, null);
            action.Invoke(context);

            validator.SetForeignException(context);
            return validator;
        }

        /// <summary>
        /// Selects an enumerable property for validation.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TNext">The type of the selected property.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a <see cref="MemberExpression"/> (e.g. <c>x => x.Property</c>) or
        ///         a <see cref="ParameterExpression"/> to select the current value (e.g. <c>x => x</c>`).
        ///     </para>
        /// </param>
        /// <param name="action">The action to perform for the selected enumerable property.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T?> AsEnumerable<T, TNext>(this IValidatorContext<T?> validator,
            Expression<Func<T, IEnumerable<TNext?>?>> selector, Action<IValidatorContext<IEnumerable<TNext?>?>> action)
            where T : class
        {
            return validator.Perform(() =>
            {
                var name = (selector.Body is ParameterExpression)
                    ? null
                    : ValidationInternals.GetPropertyName(selector);
                var value = selector.Compile().Invoke(validator.Value!);

                var context = new ValidatorContext<IEnumerable<TNext?>?>(value, validator.Path, name);
                action.Invoke(context);
            });
        }
    }
}