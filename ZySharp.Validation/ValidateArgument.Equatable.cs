using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        /// <summary>
        /// Throws if the current value is not equal to the expected value.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="value">The expected value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Equal<T>(this IValidatorContext<T> validator, T value)
        {
            return validator.Perform(() =>
            {
                if (!EqualityComparer<T>.Default.Equals(validator.Value, value))
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeValue,
                            ValidationInternals.FormatName(validator.Path, null), value));
                }
            }, false);
        }

        /// <inheritdoc cref="Equal{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> Equal<T>(this IValidatorContext<T?> validator, T value)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.Equal(value))));
        }

        /// <summary>
        /// Throws if the current value is not equal to the expected value.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="reference">A reference to the expected value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> Equal<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> reference)
        {
            ValidationInternals.ValidateNotNull(reference, nameof(reference));

            return validator.Perform(() =>
            {
                if (!EqualityComparer<T>.Default.Equals(validator.Value, reference.Value))
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeEqualToReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(reference.Path, null),
                            validator.Value, reference.Value));
                }
            }, false);
        }

        /// <inheritdoc cref="Equal{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> Equal<T>(this IValidatorContext<T?> validator,
            IArgumentReference<T> reference)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.Equal(reference))));
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
            ValidationInternals.ValidateNotNull(values, nameof(values));

            return validator.Perform(() =>
            {
                var isValidValue = values.Any(x => EqualityComparer<T>.Default.Equals(validator.Value, x));
                if (!isValidValue)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeOneOf,
                            ValidationInternals.FormatName(validator.Path, null),
                            string.Join(", ", values.Select(x => $"'{x}'").ToList())));
                }
            }, false);
        }

        /// <inheritdoc cref="Equal{T}(IValidatorContext{T},T[])"/>
        public static IValidatorContext<T?> Equal<T>(this IValidatorContext<T?> validator, params T[] values)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.Equal(values))));
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
            return validator.Perform(() =>
            {
                if (EqualityComparer<T>.Default.Equals(validator.Value, value))
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeValue,
                            ValidationInternals.FormatName(validator.Path, null), value));
                }
            }, false);
        }

        /// <inheritdoc cref="NotEqual{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> NotEqual<T>(this IValidatorContext<T?> validator, T value)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.NotEqual(value))));
        }

        /// <summary>
        /// Throws if the current value is equal to the blacklisted value.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="reference">A reference to the blacklisted value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> NotEqual<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> reference)
        {
            ValidationInternals.ValidateNotNull(reference, nameof(reference));

            return validator.Perform(() =>
            {
                if (EqualityComparer<T>.Default.Equals(validator.Value, reference.Value))
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeEqualToReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(reference.Path, null),
                            validator.Value, reference.Value));
                }
            }, false);
        }

        /// <inheritdoc cref="NotEqual{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> NotEqual<T>(this IValidatorContext<T?> validator,
            IArgumentReference<T> reference)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.NotEqual(reference))));
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
            ValidationInternals.ValidateNotNull(values, nameof(values));

            return validator.Perform(() =>
            {
                var isInvalidValue = values.Any(x => EqualityComparer<T>.Default.Equals(validator.Value, x));
                if (isInvalidValue)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeOneOf,
                            ValidationInternals.FormatName(validator.Path, null),
                            string.Join(", ", values.Select(x => $"'{x}'").ToList())));
                }
            }, false);
        }

        /// <inheritdoc cref="NotEqual{T}(IValidatorContext{T},T[])"/>
        public static IValidatorContext<T?> NotEqual<T>(this IValidatorContext<T?> validator, params T[] values)
            where T : struct
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.NotEqual(values))));
        }
    }
}