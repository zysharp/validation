using System;
using System.Globalization;

namespace ZySharp.Validation;

public static partial class ValidateArgument
{
    /// <summary>
    /// Throws if the uri does not contain an absolute uri.
    /// </summary>
    /// <typeparam name="T">The type of the current value.</typeparam>
    /// <param name="validator">The current validator context.</param>
    /// <returns>The unmodified validator context.</returns>
    public static IValidatorContext<T?> IsAbsolute<T>(this IValidatorContext<T?> validator)
        where T : Uri
    {
        return validator.Perform(() =>
        {
            if (!validator.Value!.IsAbsoluteUri)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.UriMustBeAbsolute,
                        ValidationInternals.FormatName(validator.Path, null)));
            }
        });
    }
}
