using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace ZySharp.Validation;

public static partial class ValidateArgument
{
    /// <summary>
    /// Throws if the type of the current value is not equal to the expected type.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="type">The expected type.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> HasType<T>(this IValidatorContext<T> validator, Type type)
        where T : class
    {
        ValidationInternals.ValidateNotNull(type, nameof(type));

        return validator.Perform(() =>
        {
            if (validator.Value?.GetType() != type)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeOfType,
                        ValidationInternals.FormatName(validator.Path, null), type.FullName));
            }
        }, false);
    }

    /// <summary>
    /// Throws if the type of the current value is not equal to one of the allowed types.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="types">The allowed types.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> HasType<T>(this IValidatorContext<T> validator, params Type[] types)
    {
        ValidationInternals.ValidateNotNull(types, nameof(types));

        return validator.Perform(() =>
        {
            var type = validator.Value?.GetType();
            var isValidValue = types.Any(x => (type == x));
            if (!isValidValue)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeOfTypes,
                        ValidationInternals.FormatName(validator.Path, null),
                        string.Join(", ", types.Select(x => $"'{x?.FullName ?? "null"}'").ToList())));
            }
        }, false);
    }

    /// <summary>
    /// Throws if the type of the current value is equal to the blacklisted type.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="type">The blacklisted type.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> NotHasType<T>(this IValidatorContext<T> validator, Type type)
        where T : class
    {
        ValidationInternals.ValidateNotNull(type, nameof(type));

        return validator.Perform(() =>
        {
            if (validator.Value?.GetType() == type)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeOfType,
                        ValidationInternals.FormatName(validator.Path, null), type.FullName));
            }
        }, false);
    }

    /// <summary>
    /// Throws if the type of the current value is equal to one of the blacklisted types.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="types">The blacklisted types.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> NotHasType<T>(this IValidatorContext<T> validator, params Type[] types)
    {
        ValidationInternals.ValidateNotNull(types, nameof(types));

        return validator.Perform(() =>
        {
            var type = validator.Value?.GetType();
            var isInvalidValue = types.Any(x => (type == x));
            if (isInvalidValue)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeOfTypes,
                        ValidationInternals.FormatName(validator.Path, null),
                        string.Join(", ", types.Select(x => $"'{x?.FullName ?? "null"}'").ToList())));
            }
        }, false);
    }

    /// <summary>
    /// Throws if the type of the current value is not derived from the given parent type.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="type">The parent type.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> DerivesFrom<T>(this IValidatorContext<T> validator, Type type)
        where T : class
    {
        ValidationInternals.ValidateNotNull(type, nameof(type));

        return validator.Perform(() =>
        {
            if (!type.IsAssignableFrom(validator.Value?.GetType()))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeDerivedFromType,
                        ValidationInternals.FormatName(validator.Path, null),
                        type.FullName));
            }
        });
    }

    /// <summary>
    /// Throws if the type of the current value is not derived from one of the given parent types.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="types">The allowed parent types.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> DerivesFrom<T>(this IValidatorContext<T> validator, params Type[] types)
    {
        ValidationInternals.ValidateNotNull(types, nameof(types));

        return validator.Perform(() =>
        {
            var type = validator.Value?.GetType();
            var isValidValue = types.Any(x => x.IsAssignableFrom(type));
            if (!isValidValue)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeDerivedFromTypes,
                        ValidationInternals.FormatName(validator.Path, null),
                        string.Join(", ", types.Select(x => $"'{x?.FullName ?? "null"}'").ToList())));
            }
        });
    }

    /// <summary>
    /// Throws if the type of the current value is derived from the given parent type.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="type">The blacklisted parent types.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> NotDerivesFrom<T>(this IValidatorContext<T> validator, Type type)
        where T : class
    {
        ValidationInternals.ValidateNotNull(type, nameof(type));

        return validator.Perform(() =>
        {
            if (type.IsAssignableFrom(validator.Value?.GetType()))
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeDerivedFromType,
                        ValidationInternals.FormatName(validator.Path, null),
                        type.FullName));
            }
        });
    }

    /// <summary>
    /// Throws if the type of the current value is derived from one of the blacklisted parent types.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <param name="types">The blacklisted types.</param>
    /// <returns>The unmodified validator context.</returns>
    [ExcludeFromCodeCoverage]
    public static IValidatorContext<T> NotDerivesFrom<T>(this IValidatorContext<T> validator, params Type[] types)
    {
        ValidationInternals.ValidateNotNull(types, nameof(types));

        return validator.Perform(() =>
        {
            var type = validator.Value?.GetType();
            var isInvalidValue = types.Any(x => x.IsAssignableFrom(type));
            if (isInvalidValue)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustNotBeDerivedFromTypes,
                        ValidationInternals.FormatName(validator.Path, null),
                        string.Join(", ", types.Select(x => $"'{x?.FullName ?? "null"}'").ToList())));
            }
        });
    }
}
