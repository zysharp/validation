using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ZySharp.Validation;

public static partial class ValidateArgument
{
    #region NotNull

    /// <summary>
    /// Throws if the current value is <c>null</c>.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<T?> NotNull<T>(this IValidatorContext<T?> validator)
    {
        return validator.Perform(() =>
        {
            if (validator.Value is null)
            {
                validator.SetArgumentNullException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNull,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        }, false);
    }

    /// <inheritdoc cref="NotNull{T}(IValidatorContext{T})"/>
    public static IValidatorContext<T?> NotNull<T>(this IValidatorContext<T?> validator)
        where T : struct
    {
        return validator.Perform(() =>
        {
            if (!validator.Value.HasValue)
            {
                validator.SetArgumentNullException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNull,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        }, false);
    }

    #endregion NotNull

    #region NotEmpty

    /// <summary>
    /// Throws if the current value is empty (contains the default value of the current type).
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<T> NotEmpty<T>(this IValidatorContext<T> validator)
    {
        return validator.Perform(() =>
        {
            if (Equals(validator.Value, default(T)))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        });
    }

    /// <inheritdoc cref="NotEmpty{T}(IValidatorContext{T})"/>
    public static IValidatorContext<T?> NotEmpty<T>(this IValidatorContext<T?> validator)
        where T : struct
    {
        return validator.Perform(() =>
            validator.When(x => x.HasValue, v => v.Select(x => x!.Value, v => v.NotEmpty())));
    }

    /// <summary>
    /// Throws if the enumerable contains no elements.
    /// </summary>
    /// <typeparam name="TValue">The type of the enumerable elements.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<IEnumerable<TValue>?> NotEmpty<TValue>(
        this IValidatorContext<IEnumerable<TValue>?> validator)
    {
        return validator.Perform(() =>
        {
            if (!validator.Value!.Any())
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        });
    }

    /// <summary>
    /// Throws if the string contains no characters.
    /// </summary>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<string?> NotEmpty(this IValidatorContext<string?> validator)
    {
        return validator.Perform(() =>
        {
            if (validator.Value!.Length == 0)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        });
    }

    #endregion NotEmpty

    #region NotNullOrEmpty

    /// <summary>
    /// Throws if the current value is <c>null</c> or empty (contains the default value of the current type).
    /// <para>
    ///     This validation as well applies to empty strings or <see cref="IEnumerable{T}"/> types without at
    ///     least one element.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<T?> NotNullOrEmpty<T>(this IValidatorContext<T?> validator)
    {
        return validator.Perform(() =>
        {
            if (validator.Value is null)
            {
                validator.SetArgumentNullException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNullOrEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
                return;
            }
            if (validator.Value.Equals(default(T)))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNullOrEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
                return;
            }

            var isEmptyEnum = (validator.Value is IEnumerable enumerable) && !enumerable.GetEnumerator().MoveNext();
            var isEmptyString = validator.Value is string { Length: 0 };

            if (isEmptyEnum || isEmptyString)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNullOrEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        }, false);
    }

    /// <inheritdoc cref="NotNullOrEmpty{T}(IValidatorContext{T})"/>
    public static IValidatorContext<T?> NotNullOrEmpty<T>(this IValidatorContext<T?> validator)
        where T : struct
    {
        return validator.Perform(() =>
        {
            if (!validator.Value.HasValue)
            {
                validator.SetArgumentNullException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNullOrEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
            if (validator.Value.Equals(default(T)))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeNullOrEmpty,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        }, false);
    }

    #endregion NotNullOrEmpty

    #region MutuallyExclusive

    /// <summary>
    /// Throws if both, the current value and the <paramref name="reference"/> value is not <c>null</c>.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="reference">A reference to the other value.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<T?> MutuallyExclusive<T>(this IValidatorContext<T?> validator,
        IArgumentReference<T?> reference)
    {
        ValidationInternals.ValidateNotNull(reference, nameof(reference));

        return validator.Perform(() =>
        {
            if ((validator.Value is not null) && (reference.Value is not null))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeMutuallyExclusive,
                        ValidationInternals.FormatName(validator.Path, null),
                        ValidationInternals.FormatName(reference.Path, null)));
            }
        }, false);
    }

    #endregion
}
