using Xunit;

namespace ZySharp.Validation.Tests
{
    public static class TestExtensions
    {
        public static IValidatorContext<T> AssertEqualValueAndPath<T>(this IValidatorContext<T> validator, T value, string path)
        {
            Assert.Equal(value, validator.Value);
            Assert.Equal(path, string.Join('.', validator.Path));

            return validator;
        }

        public static IValidatorContext<T> AssertEqualValueAndPathIndexed<T>(this IValidatorContext<T> validator, T value, string path,
            ref int index)
        {
            ++index;

            return validator.AssertEqualValueAndPath(value, path);
        }
    }
}