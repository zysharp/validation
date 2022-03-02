using System;
using System.Globalization;

namespace ZySharp.Validation
{
    public static partial class ValidateArgument
    {
        #region GreaterThan

        /// <summary>
        /// Throws if the current value is not greater than the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThan<T>(this IValidatorContext<T> validator, T threshold)
            where T : IComparable<T>
        {
            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold) <= 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeGreaterThan,
                            ValidationInternals.FormatName(validator.Path, null), threshold));
                }
            });
        }

        /// <inheritdoc cref="GreaterThan{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> GreaterThan<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThan(threshold))));
        }

        /// <summary>
        /// Throws if the current value is not greater than the given threshold.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">A reference to the threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThan<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> threshold)
            where T : IComparable<T>
        {
            ValidationInternals.ValidateNotNull(threshold, nameof(threshold));

            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold.Value) <= 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeGreaterThanReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(threshold.Path, null),
                            validator.Value, threshold.Value));
                }
            });
        }

        /// <inheritdoc cref="GreaterThan{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> GreaterThan<T>(this IValidatorContext<T?> validator,
            IArgumentReference<T> threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThan(threshold))));
        }

        #endregion GreaterThan

        #region GreaterThanOrEqualTo

        /// <summary>
        /// Throws if the current value is not greater than or equal to the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThanOrEqualTo<T>(this IValidatorContext<T> validator, T threshold)
            where T : IComparable<T>
        {
            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold) < 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeGreaterThanOrEqualTo,
                            ValidationInternals.FormatName(validator.Path, null), threshold));
                }
            });
        }

        /// <inheritdoc cref="GreaterThanOrEqualTo{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> GreaterThanOrEqualTo<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThanOrEqualTo(threshold))));
        }

        /// <summary>
        /// Throws if the current value is not greater than or equal to the given threshold.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">A reference to the threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> GreaterThanOrEqualTo<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> threshold)
            where T : IComparable<T>
        {
            ValidationInternals.ValidateNotNull(threshold, nameof(threshold));

            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold.Value) < 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeGreaterThanOrEqualToReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(threshold.Path, null),
                            validator.Value, threshold.Value));
                }
            });
        }

        /// <inheritdoc cref="GreaterThanOrEqualTo{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> GreaterThanOrEqualTo<T>(this IValidatorContext<T?> validator,
            IArgumentReference<T> threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.GreaterThanOrEqualTo(threshold))));
        }

        #endregion GreaterThanOrEqualTo

        #region LessThan

        /// <summary>
        /// Throws if the current value is not less than the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThan<T>(this IValidatorContext<T> validator, T threshold)
            where T : IComparable<T>
        {
            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold) >= 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeLessThan,
                            ValidationInternals.FormatName(validator.Path, null), threshold));
                }
            });
        }

        /// <inheritdoc cref="LessThan{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> LessThan<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThan(threshold)));
        }

        /// <summary>
        /// Throws if the current value is not less than the given threshold.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">A reference to the threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThan<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> threshold)
            where T : IComparable<T>
        {
            ValidationInternals.ValidateNotNull(threshold, nameof(threshold));

            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold.Value) >= 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeLessThanReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(threshold.Path, null),
                            validator.Value, threshold.Value));
                }
            });
        }

        /// <inheritdoc cref="LessThan{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> LessThan<T>(this IValidatorContext<T?> validator, IArgumentReference<T> threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThan(threshold))));
        }

        #endregion LessThan

        #region LessThanOrEqualTo

        /// <summary>
        /// Throws if the current value is not less than or equal to the given threshold.
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">The threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThanOrEqualTo<T>(this IValidatorContext<T> validator, T threshold)
            where T : IComparable<T>
        {
            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold) > 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeLessThanOrEqualTo,
                            ValidationInternals.FormatName(validator.Path, null), threshold));
                }
            });
        }

        /// <inheritdoc cref="LessThanOrEqualTo{T}(IValidatorContext{T},T)"/>
        public static IValidatorContext<T?> LessThanOrEqualTo<T>(this IValidatorContext<T?> validator, T threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThanOrEqualTo(threshold))));
        }

        /// <summary>
        /// Throws if the current value is not less than or equal to the given threshold.
        /// <para>
        ///     This overload should be used if the right side is an argument reference rather than
        ///     a constant value.
        /// </para>
        /// </summary>
        /// <typeparam name="T">The type of the current value.</typeparam>
        /// <param name="validator">The current validator context.</param>
        /// <param name="threshold">A reference to the threshold value.</param>
        /// <returns>The unmodified validator context.</returns>
        public static IValidatorContext<T> LessThanOrEqualTo<T>(this IValidatorContext<T> validator,
            IArgumentReference<T> threshold)
            where T : IComparable<T>
        {
            ValidationInternals.ValidateNotNull(threshold, nameof(threshold));

            return validator.Perform(() =>
            {
                if (validator.Value.CompareTo(threshold.Value) > 0)
                {
                    validator.SetArgumentException(
                        string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeLessThanOrEqualToReference,
                            ValidationInternals.FormatName(validator.Path, null),
                            ValidationInternals.FormatName(threshold.Path, null),
                            validator.Value, threshold.Value));
                }
            });
        }

        /// <inheritdoc cref="LessThanOrEqualTo{T}(IValidatorContext{T},IArgumentReference{T})"/>
        public static IValidatorContext<T?> LessThanOrEqualTo<T>(this IValidatorContext<T?> validator,
            IArgumentReference<T> threshold)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.LessThanOrEqualTo(threshold))));
        }

        #endregion LessThanOrEqualTo

        #region InRange

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
            where T : IComparable<T>
        {
            ValidationInternals.ValidateNotNull(validator, nameof(validator));

            if (validator.Exception is not null)
            {
                return validator;
            }

            validator.GreaterThanOrEqualTo(min);
            validator.LessThanOrEqualTo(max);

            if (validator.Exception is not null)
            {
                validator.SetArgumentException(
                    string.Format(CultureInfo.InvariantCulture, Resources.ArgumentMustBeInRange,
                        ValidationInternals.FormatName(validator.Path, null), min, max));
            }

            return validator;
        }

        /// <inheritdoc cref="InRange{T}(IValidatorContext{T},T,T)"/>
        public static IValidatorContext<T?> InRange<T>(this IValidatorContext<T?> validator, T min, T max)
            where T : struct, IComparable<T>
        {
            return validator.Perform(() =>
                validator.When(x => x.HasValue, v => v.Select(x => x.Value, v => v.InRange(min, max))));
        }

        #endregion InRange
    }
}