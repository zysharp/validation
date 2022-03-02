using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        /// <summary>
        /// Throws if the enumerable contains duplicate values.
        /// </summary>
        /// <typeparam name="TValue">The type of the enumerable elements.</typeparam>
        /// <typeparam name="TKey">The type of the enumerable element keys.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<IEnumerable<TValue>> HasDistinctElements<TValue, TKey>(
            this IValidatorContext<IEnumerable<TValue>> validator, Func<TValue, TKey> keySelector)
        {
            ValidationInternals.ValidateNotNull(keySelector, nameof(keySelector));

            return validator.Perform(() =>
            {
                var duplicates = validator.Value
                    .GroupBy(keySelector)
                    .Where(x => (x.Count() != 1))
                    .Select(x => x.Key)
                    .ToList();

                if (duplicates.Any())
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.EnumerableMustBeDistinct,
                            ValidationInternals.FormatName(validator.Path, null),
                            string.Join(", ", duplicates.Select(x => $"'{x}'").ToList())));
                }
            });
        }
    }
}