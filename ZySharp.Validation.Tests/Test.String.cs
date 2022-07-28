using System;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestString
    {
        [Fact]
        public void MustMatchPattern()
        {
            var test = "test";

            ValidateArgument.For(test, nameof(test), v => v
                .MustMatchPattern(".*")
            );

            Assert.Throws<ArgumentException>(() => ValidateArgument.For(test, nameof(test), v => v
                .MustMatchPattern("[0-9]")
            ));
        }
    }
}