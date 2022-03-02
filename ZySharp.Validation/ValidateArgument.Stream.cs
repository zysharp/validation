using System.Globalization;
using System.IO;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        /// <summary>
        /// Throws if the stream is not readable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> IsReadable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            return validator.Perform(() =>
            {
                if (!validator.Value.CanRead)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.StreamMustBeReadable,
                            ValidationInternals.FormatName(validator.Path, null)));
                }
            });
        }

        /// <summary>
        /// Throws if the stream is not writable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> IsWritable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            return validator.Perform(() =>
            {
                if (!validator.Value.CanWrite)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.StreamMustBeWriteable,
                            ValidationInternals.FormatName(validator.Path, null)));
                }
            });
        }

        /// <summary>
        /// Throws if the stream is not seekable.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> IsSeekable<T>(this IValidatorContext<T> validator)
            where T : Stream
        {
            return validator.Perform(() =>
            {
                if (!validator.Value.CanSeek)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.StreamMustBeSeekable,
                            ValidationInternals.FormatName(validator.Path, null)));
                }
            });
        }
    }
}