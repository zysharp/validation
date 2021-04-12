using System;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestValidationEquality
    {
        [Theory]
        [InlineData(DateTimeKind.Local, DateTimeKind.Local)]
        public void Test_Equal_Valid(DateTimeKind expected, DateTimeKind value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .Equal(expected);
        }

        [Theory]
        [InlineData(DateTimeKind.Local, DateTimeKind.Unspecified)]
        [InlineData(DateTimeKind.Local, DateTimeKind.Utc)]
        public void Test_Equal_Throws(DateTimeKind expected, DateTimeKind value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .Equal(expected);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Local)]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Unspecified)]
        public void Test_Equal_List_Valid(DateTimeKind[] whitelist, DateTimeKind value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .Equal(whitelist);
        }

        [Theory]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Utc)]
        public void Test_Equal_List_Throws(DateTimeKind[] blacklist, DateTimeKind value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .Equal(blacklist);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(DateTimeKind.Local, DateTimeKind.Unspecified)]
        [InlineData(DateTimeKind.Local, DateTimeKind.Utc)]
        public void Test_NotEqual_Valid(DateTimeKind disallowed, DateTimeKind value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotEqual(disallowed);
        }

        [Theory]
        [InlineData(DateTimeKind.Local, DateTimeKind.Local)]
        public void Test_NotEqual_Throws(DateTimeKind disallowed, DateTimeKind value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotEqual(disallowed);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }

        [Theory]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Utc)]
        public void Test_NotEqual_List_Valid(DateTimeKind[] blacklist, DateTimeKind value)
        {
            ValidateArgument
                .For(value, nameof(value))
                .NotEqual(blacklist);
        }

        [Theory]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Local)]
        [InlineData(new[] { DateTimeKind.Local, DateTimeKind.Unspecified }, DateTimeKind.Unspecified)]
        public void Test_NotEqual_List_Throws(DateTimeKind[] blacklist, DateTimeKind value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                ValidateArgument
                    .For(value, nameof(value))
                    .NotEqual(blacklist);
            });

            Assert.Equal(nameof(value), exception.ParamName);
        }
    }
}