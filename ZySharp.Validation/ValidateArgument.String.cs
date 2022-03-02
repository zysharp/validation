using System.Globalization;
using System.Text.RegularExpressions;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        /// <summary>
        /// Throws if the string does not match the given pattern.
        /// </summary>
        /// <param name="validator">The current validator context.</param>
        /// <param name="regex">The regular expression containing the pattern.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<string> MustMatchPattern(this IValidatorContext<string> validator, Regex regex)
        {
            ValidationInternals.ValidateNotNull(validator, nameof(regex));

            return validator.Perform(() =>
            {
                if (!regex.IsMatch(validator.Value))
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.StringMustMatchPattern,
                            ValidationInternals.FormatName(validator.Path, null), regex));
                }
            });
        }

        /// <inheritdoc cref="MustMatchPattern(IValidatorContext{string},Regex)"/>
        public static IValidatorContext<string> MustMatchPattern(this IValidatorContext<string> validator, string regex)
        {
            ValidationInternals.ValidateNotNull(regex, nameof(regex));

            return MustMatchPattern(validator, new Regex(regex));
        }
    }
}