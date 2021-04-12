using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestValidationBasic
    {
        public static IEnumerable<object[]> DataNotNull => new List<object[]>
        {
            new[] { new object()    },
            new[] { string.Empty    },
            new[] { "test_not_null" },
            new[] { new List<int>() }
        };

        [Theory]
        [MemberData(nameof(DataNotNull))]
        public void Test_NotNull_Valid(object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotNull();
        }

        [Theory]
        [InlineData(null)]
        public void Test_NotNull_Throws(object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotNull();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1337)]
        public void Test_NotNull_Nullable_Valid(int? value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotNull();
        }

        [Theory]
        [InlineData(null)]
        public void Test_NotNull_Nullable_Throws(int? value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotNull();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(1337)]
        public void Test_NotEmpty_Valid(int value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotEmpty();
        }

        [Theory]
        [InlineData(0)]
        public void Test_NotEmpty_Throws(int value)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotEmpty();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(1337)]
        public void Test_NotEmpty_Nullable_Valid(int? value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotEmpty();
        }

        [Theory]
        [InlineData(0)]
        public void Test_NotEmpty_Nullable_Throws(int? value)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotEmpty();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        public static IEnumerable<object[]> DataNotNullOrEmpty => new List<object[]>
        {
            new[] { new object()    },
            new[] { "test_not_null" }
        };

        [Theory]
        [MemberData(nameof(DataNotNullOrEmpty))]
        public void Test_NotNullOrEmpty_Valid(object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotNullOrEmpty();
        }

        [Theory]
        [InlineData(null)]
        public void Test_NotNullOrEmpty_Throws_NotNull(object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotNullOrEmpty();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        public static IEnumerable<object[]> DataNotNullOrEmptyThrows => new List<object[]>
        {
            new[] { string.Empty    },
            new[] { new List<int>() }
        };

        [Theory]
        [MemberData(nameof(DataNotNullOrEmptyThrows))]
        public void Test_NotNullOrEmpty_Throws_NotEmpty(object value)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotNullOrEmpty();
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }
    }
}