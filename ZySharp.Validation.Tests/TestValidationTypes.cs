using System;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestValidationTypes
    {
        [Theory]
        [InlineData(typeof(string), "test")]
        public void Test_HasType_Valid(Type expected, object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .HasType(expected);
        }

        [Theory]
        [InlineData(typeof(string), null)]
        [InlineData(typeof(string), 1337)]
        public void Test_HasType_Throws(Type expected, object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .HasType(expected);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(new[] { typeof(string), typeof(int) }, "test")]
        [InlineData(new[] { typeof(string), typeof(int) }, 1337)]
        public void Test_HasType_List_Valid(Type[] whitelist, object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .HasType(whitelist);
        }

        [Theory]
        [InlineData(new[] { typeof(string), typeof(int) }, DateTimeKind.Utc)]
        public void Test_HasType_List_Throws(Type[] blacklist, object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .HasType(blacklist);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(typeof(string), 1337)]
        [InlineData(typeof(string), null)]
        public void Test_NotHasType_Valid(Type disallowed, object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotHasType(disallowed);
        }

        [Theory]
        [InlineData(typeof(string), "test")]
        public void Test_NotHasType_Throws(Type disallowed, object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotHasType(disallowed);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(new[] { typeof(string), typeof(int) }, DateTimeKind.Utc)]
        public void Test_NotHasType_List_Valid(Type[] blacklist, object value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotHasType(blacklist);
        }

        [Theory]
        [InlineData(new[] { typeof(string), typeof(int) }, "test")]
        [InlineData(new[] { typeof(string), typeof(int) }, 1337)]
        public void Test_NotHasType_List_Throws(Type[] blacklist, object value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotHasType(blacklist);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }
    }
}