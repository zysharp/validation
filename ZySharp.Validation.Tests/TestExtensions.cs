using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

using Xunit;

namespace ZySharp.Validation.Tests;

[ExcludeFromCodeCoverage]
internal static class TestExtensions
{
    public static IValidatorContext<T?> AssertEqualValueAndPath<T>(this IValidatorContext<T?> validator, T value, string path)
    {
        Contract.Assert(validator is not null);
        Contract.Assert(!string.IsNullOrEmpty(path));

        Assert.Equal(value, validator.Value);
#if NETFRAMEWORK
        Assert.Equal(path, string.Join(".", validator.Path));
#else
        Assert.Equal(path, string.Join('.', validator.Path));
#endif

        return validator;
    }

    public static IArgumentReference<T?> AssertEqualValueAndPath<T>(this IArgumentReference<T?> reference, T value, string path)
    {
        Contract.Assert(reference is not null);
        Contract.Assert(!string.IsNullOrEmpty(path));

        Assert.Equal(value, reference.Value);
#if NETFRAMEWORK
        Assert.Equal(path, string.Join(".", reference.Path));
#else
        Assert.Equal(path, string.Join('.', reference.Path));
#endif

        return reference;
    }

    public static IValidatorContext<T?> AssertEqualValueAndPathIndexed<T>(this IValidatorContext<T?> validator, T value,
        string path, ref int index)
    {
        Contract.Assert(validator is not null);
        Contract.Assert(!string.IsNullOrEmpty(path));

        ++index;

        return validator.AssertEqualValueAndPath(value, path);
    }

    public static void TestValidation<TValue>(TValue value, Func<IValidatorContext<TValue?>, IValidatorContext<TValue?>> action,
        bool expectThrow, Type? exceptionType = null)
    {
        Contract.Assert(action is not null);

        if (expectThrow)
        {
            exceptionType ??= typeof(ArgumentException);

            var exception = Assert.Throws(exceptionType, () =>
                ValidateArgument.For(value, nameof(value), v => action(v)
                    .AssertEqualValueAndPath(value, nameof(value))));

#if NET6_0_OR_GREATER
            // Ensure that the validator method is excluded from the stacktrace string
            Assert.DoesNotContain(nameof(ValidateArgument), exception.StackTrace, StringComparison.InvariantCultureIgnoreCase);
#else
            _ = exception;
#endif
        }
        else
        {
            var result = ValidateArgument.For(value, nameof(value), v => action(v));
            Assert.Equal(value, result);
        }
    }

    public static void TestValidation<TValue>(RefTestCase<TValue> test,
        Func<IValidatorContext<TValue?>, IValidatorContext<TValue?>> action)
        where TValue : class
    {
        TestValidation(test.Value, action, test.ExpectThrow, test.ExceptionType);
    }

    public static void TestValidation<TValue>(ValTestCase<TValue> test,
        Func<IValidatorContext<TValue?>, IValidatorContext<TValue?>> action)
        where TValue : struct
    {
        TestValidation(test.Value, action, test.ExpectThrow, test.ExceptionType);
    }
}

[ExcludeFromCodeCoverage]
public class RefTestCase<TValue>
    where TValue : class
{
    public TValue? Value { get; }

    public bool ExpectThrow { get; }

    public Type? ExceptionType { get; }

    public RefTestCase(TValue? value, bool expectThrow, Type? exceptionType = null)
    {
        Value = value;
        ExpectThrow = expectThrow;
        ExceptionType = exceptionType;
    }
}

[ExcludeFromCodeCoverage]
public class ValTestCase<TValue>
    where TValue : struct
{
    public TValue? Value { get; }

    public bool ExpectThrow { get; }

    public Type? ExceptionType { get; }

    public ValTestCase(TValue? value, bool expectThrow, Type? exceptionType = null)
    {
        Value = value;
        ExpectThrow = expectThrow;
        ExceptionType = exceptionType;
    }
}

[ExcludeFromCodeCoverage]
public class RefTestCaseWithParam<TValue, TParam> :
    RefTestCase<TValue>
    where TValue : class
{
    public TParam? Parameter { get; }

    public RefTestCaseWithParam(TValue? value, TParam? parameter, bool expectThrow, Type? exceptionType = null) :
        base(value, expectThrow, exceptionType)
    {
        Parameter = parameter;
    }
}

[ExcludeFromCodeCoverage]
public class ValTestCaseWithParam<TValue, TParam> :
    ValTestCase<TValue>
    where TValue : struct
{
    public TParam? Parameter { get; }

    public ValTestCaseWithParam(TValue? value, TParam? parameter, bool expectThrow, Type? exceptionType = null) :
        base(value, expectThrow, exceptionType)
    {
        Parameter = parameter;
    }
}

[ExcludeFromCodeCoverage]
public class ValTestCaseWithValParam<TValue, TParam> :
    ValTestCase<TValue>
    where TValue : struct
    where TParam : struct
{
    public TParam? Parameter { get; }

    public ValTestCaseWithValParam(TValue? value, TParam? parameter, bool expectThrow, Type? exceptionType = null) :
        base(value, expectThrow, exceptionType)
    {
        Parameter = parameter;
    }
}
