using System;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestValidationRanges
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 0, 2)]
        [InlineData(0, -1, 1)]
        public void Test_InRange_Valid(int value, int min, int max)
        {
            ValidateArgument
                .For(value, nameof(value))
                .InRange(min, max);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 2)]
        [InlineData(0, -2, -1)]
        public void Test_InRange_Throws(int value, int min, int max)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .InRange(min, max);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(null, -1, 1)]
        [InlineData(0, 0, 0)]
        [InlineData(0, 0, 1)]
        [InlineData(1, 0, 2)]
        [InlineData(0, -1, 1)]
        public void Test_InRange_Nullable_Valid(int? value, int min, int max)
        {
            ValidateArgument
                .For(value, nameof(value))
                .InRange(min, max);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(0, 1, 2)]
        [InlineData(0, -2, -1)]
        public void Test_InRange_Nullable_Throws(int? value, int min, int max)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .InRange(min, max);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }
    }
}