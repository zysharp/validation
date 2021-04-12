using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace ZySharp.Validation
{
    public static class ValidateArgument
    {
        #region Selector Methods

        /// <summary>
        /// Selects an argument for validation.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="value">The value of the argument to validate.</param>
        /// <param name="name">The name of the argument to validate.</param>
        /// <returns>A validator context for the given argument.</returns>
        public static IValidatorContext<T> For<T>([ValidatedNotNull][NoEnumeration]T value, string name)
        {
            ValidationInternals.ValidateNotNull(name, nameof(name));

            return new ValidatorContext<T>(value, name);
        }

        /// <summary>
        /// Selects a property for validation.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TNext">The type of the selected property.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a `MemberExpression` (e.g. `x => x.Property`).
        ///     </para>
        /// </param>
        /// <param name="action">The action to perform for the selected property.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> For<T, TNext>(this IValidatorContext<T> validator,
            Expression<Func<T, TNext>> selector, Action<IValidatorContext<TNext>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            var name = ValidationInternals.GetPropertyName(selector);
            var value = selector.Compile().Invoke(validator.Value);

            var context = new ValidatorContext<TNext>(value, validator.Path, name);
            action.Invoke(context);

            return validator;
        }

        /// <summary>
        /// Selects all elements of a `IEnumerable` property for validation.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <typeparam name="TItem">The type of an individual item in the `IEnumerable` property.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="selector">
        ///     The property selector expression.
        ///     <para>
        ///         The expression must be a `MemberExpression` (e.g. `x => x.Property`) or a `ParameterExpression`
        ///         to select all elements of the current value (e.g. `x => x`).
        ///     </para>
        /// </param>
        /// <param name="action">The action to perform for each item of the selected `IEnumerable` property.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> ForEach<T, TItem>(this IValidatorContext<T> validator,
            Expression<Func<T, IEnumerable<TItem>>> selector, Action<IValidatorContext<TItem>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            var name = (selector.Body is ParameterExpression)
                ? null
                : ValidationInternals.GetPropertyName(selector);
            var value = selector.Compile().Invoke(validator.Value);

            if (value is null)
            {
                return validator;
            }

            int i = 0;
            foreach (var item in value)
            {
                var context = (name is null)
                    ? new ValidatorContext<TItem>(item, $"{ValidationInternals.FormatName(validator.Path, null)}[{i++}]")
                    : new ValidatorContext<TItem>(item, validator.Path, $"{name}[{i++}]");
                action.Invoke(context);
            }

            return validator;
        }

        /// <summary>
        /// Performs validations if a user-defined condition is met.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="predicate">The filter predicate.</param>
        /// <param name="action">The action to perform if the condition is met.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> When<T>(this IValidatorContext<T> validator, Func<T, bool> predicate,
            Action<IValidatorContext<T>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(predicate, nameof(predicate));
            ValidationInternals.ValidateNotNull(action, nameof(action));

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
        public static IValidatorContext<T> Select<T, TNew>(this IValidatorContext<T> validator, Func<T, TNew> selector,
            Action<IValidatorContext<TNew>> action)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));
            ValidationInternals.ValidateNotNull(selector, nameof(selector));
            ValidationInternals.ValidateNotNull(action, nameof(action));

            var context = new ValidatorContext<TNew>(selector.Invoke(validator.Value), validator.Path, null);
            action.Invoke(context);

            return validator;
        }

        #endregion Selector Methods

        #region Validation: Basic

        /// <summary>
        /// Throws if the current value is `null`.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotNull<T>(this IValidatorContext<T> validator)
            where T : class
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value is null)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNull, ValidationInternals.FormatName(validator.Path, null)));
            }

            return validator;
        }

        /// <inheritdoc cref="NotNull{T}(ZySharp.Validation.IValidatorContext{T})"/>
        public static IValidatorContext<T?> NotNull<T>(this IValidatorContext<T?> validator)
            where T : struct
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.HasValue)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNull, ValidationInternals.FormatName(validator.Path, null)));
            }

            return validator;
        }

        /// <summary>
        /// Throws if the current value is empty (contains the default value of the current type).
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotEmpty<T>(this IValidatorContext<T> validator)
            where T : struct
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value.Equals(default(T)))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                        Resources.ArgumentMustNotBeEmpty, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="NotEmpty{T}(ZySharp.Validation.IValidatorContext{T})"/>
        public static IValidatorContext<T?> NotEmpty<T>(this IValidatorContext<T?> validator)
            where T : struct
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.NotEmpty()));
        }

        /// <summary>
        /// Throws if the current value is `null` or empty (contains the default value of the current type).
        /// <para>
        ///     This validation as well applies to empty strings or `IEnumerable` types without at least one element.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotNullOrEmpty<T>(this IValidatorContext<T> validator)
            where T : class
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            try
            {
                validator.NotNull();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNullOrEmpty, ValidationInternals.FormatName(validator.Path, null)));
            }

            var isEmptyEnum = (validator.Value is IEnumerable enumerable) && !enumerable.GetEnumerator().MoveNext();
            var isEmptyString = validator.Value is string { Length: 0 };

            if (isEmptyEnum || isEmptyString)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                        Resources.ArgumentMustNotBeNullOrEmpty, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="NotNullOrEmpty{T}(ZySharp.Validation.IValidatorContext{T})"/>
        public static IValidatorContext<T?> NotNullOrEmpty<T>(this IValidatorContext<T?> validator)
            where T : struct
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.HasValue)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                        Resources.ArgumentMustNotBeNullOrEmpty, ValidationInternals.FormatName(validator.Path, null)));
            }

            try
            {
                validator.NotEmpty();
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeNullOrEmpty, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        #endregion Validation: Basic

        #region Validation: Equality

        /// <summary>
        /// Throws if the current value is not equal to the expected value.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Equal<T>(this IValidatorContext<T> validator, T value)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!EqualityComparer<T>.Default.Equals(validator.Value, value))
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeValue, ValidationInternals.FormatName(validator.Path, null), value));
            }

            return validator;
        }

        /// <inheritdoc cref="Equal{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> Equal<T>(this IValidatorContext<T?> validator, T value)
            where T : struct
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.Equal(value)));
        }

        /// <summary>
        /// Throws if the current value is not equal to one of the allowed values.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="values">The allowed values.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Equal<T>(this IValidatorContext<T> validator, params T[] values)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            var isValidValue = values.Any(x => EqualityComparer<T>.Default.Equals(validator.Value, x));
            if (!isValidValue)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeOneOf, ValidationInternals.FormatName(validator.Path, null),
                    string.Join(", ", values.Select(x => $"'{x}'").ToList())));
            }

            return validator;
        }

        /// <inheritdoc cref="Equal{T}(ZySharp.Validation.IValidatorContext{T},T[])"/>
        public static IValidatorContext<T?> Equal<T>(this IValidatorContext<T?> validator, params T[] values)
            where T : struct
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.Equal(values)));
        }

        /// <summary>
        /// Throws if the current value is equal to the blacklisted value.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="value">The blacklisted value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotEqual<T>(this IValidatorContext<T> validator, T value)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (EqualityComparer<T>.Default.Equals(validator.Value, value))
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeValue, ValidationInternals.FormatName(validator.Path, null), value));
            }

            return validator;
        }

        /// <inheritdoc cref="NotEqual{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> NotEqual<T>(this IValidatorContext<T?> validator, T value)
            where T : struct
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.Equal(value)));
        }

        /// <summary>
        /// Throws if the current value is equal to one of the blacklisted values.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="values">The blacklisted values.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotEqual<T>(this IValidatorContext<T> validator, params T[] values)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            var isValidValue = values.All(x => !EqualityComparer<T>.Default.Equals(validator.Value, x));
            if (!isValidValue)
            {
                throw new ArgumentNullException(validator.Path.First(), string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustNotBeOneOf, ValidationInternals.FormatName(validator.Path, null),
                    string.Join(", ", values.Select(x => $"'{x}'").ToList())));
            }

            return validator;
        }

        /// <inheritdoc cref="NotEqual{T}(ZySharp.Validation.IValidatorContext{T},T[])"/>
        public static IValidatorContext<T?> NotEqual<T>(this IValidatorContext<T?> validator, params T[] values)
            where T : struct
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.Equal(values)));
        }

        #endregion Validation: Equality

        #region Validation: Ranges

        /// <summary>
        /// Throws if the current value is not in the given range.
        /// <para>
        ///     This range check is "inclusive" which means that the lower- and upper- limits are both valid values.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> InRange<T>(this IValidatorContext<T> validator, T min, T max)
            where T : struct, IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            try
            {
                validator.GreaterThanOrEqualTo(min);
                validator.LessThanOrEqualTo(max);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeInRange, ValidationInternals.FormatName(validator.Path, null), min, max),
                    validator.Path.First(), e);
            }

            return validator;
        }

        /// <inheritdoc cref="InRange{T}(ZySharp.Validation.IValidatorContext{T},T,T)"/>
        public static IValidatorContext<T?> InRange<T>(this IValidatorContext<T?> validator, T min, T max)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.InRange(min, max)));
        }

        /// <summary>
        /// Throws if the current value is not greater than the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThan<T>(this IValidatorContext<T> validator, T threshold)
            where T : struct, IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value.CompareTo(threshold) <= 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeGreaterThan, ValidationInternals.FormatName(validator.Path, null), threshold),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="GreaterThan{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> GreaterThan<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThan(threshold)));
        }

        /// <summary>
        /// Throws if the current value is not greater than or equal to the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThanOrEqualTo<T>(this IValidatorContext<T> validator, T threshold)
            where T : struct, IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value.CompareTo(threshold) < 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeGreaterThan, ValidationInternals.FormatName(validator.Path, null), threshold),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="GreaterThanOrEqualTo{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> GreaterThanOrEqualTo<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThanOrEqualTo(threshold)));
        }

        /// <summary>
        /// Throws if the current value is not less than the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThan<T>(this IValidatorContext<T> validator, T threshold)
            where T : struct, IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value.CompareTo(threshold) >= 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeGreaterThan, ValidationInternals.FormatName(validator.Path, null), threshold),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="LessThan{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> LessThan<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThan(threshold)));
        }

        /// <summary>
        /// Throws if the current value is not less than or equal to the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThanOrEqualTo<T>(this IValidatorContext<T> validator, T threshold)
            where T : struct, IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Value.CompareTo(threshold) > 0)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.ArgumentMustBeGreaterThan, ValidationInternals.FormatName(validator.Path, null), threshold),
                    validator.Path.First());
            }

            return validator;
        }

        /// <inheritdoc cref="LessThanOrEqualTo{T}(ZySharp.Validation.IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> LessThanOrEqualTo<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThanOrEqualTo(threshold)));
        }

        #endregion Validation: Ranges

        #region Validation: Stream

        /// <summary>
        /// Throws if the stream is not readable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Readable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.CanRead)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.StreamMustBeReadable, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        /// <summary>
        /// Throws if the stream is not writable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Writeable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.CanWrite)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.StreamMustBeWriteable, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        /// <summary>
        /// Throws if the stream is not seekable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Seekable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.CanSeek)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                    Resources.StreamMustBeSeekable, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        #endregion Validation: Stream

        #region Validation: Uri

        /// <summary>
        /// Throws if the uri does not contain an absolute uri.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Absolute<T>(this IValidatorContext<T> validator)
            where T : Uri
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (!validator.Value.IsAbsoluteUri)
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture,
                        Resources.UriMustBeAbsolute, ValidationInternals.FormatName(validator.Path, null)),
                    validator.Path.First());
            }

            return validator;
        }

        #endregion Validation: Uri
    }
}