using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests
{
    [Trait("Category", "Unit")]
    public sealed class TestSelectors
    {
        [Fact]
        public void For()
        {
            var test = new KeyValuePair<int, string>(1337, "test");
            var path = $"{nameof(test)}.{nameof(test.Key)}";

            ValidateArgument.For(test, nameof(test), v => v
                .For(x => x.Key, v => v
                    .AssertEqualValueAndPath(test.Key, path)
                )
            );
        }

        [Fact]
        public void ForRequiresMember()
        {
            var test = new KeyValuePair<int, string>(1337, "test");

            Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument.For(test, nameof(test), v => v
                    .For(x => x.ToString(), v => v.NotNull()
                    )
                );
            });
        }

        [Fact]
        public void ForEach()
        {
            var test = new List<int> { 1, 2, 3 };
            var path = nameof(test);

            var i = 0;

            ValidateArgument.For(test, nameof(test), v => v
                .ForEach(x => x, v => v
                    .AssertEqualValueAndPathIndexed(test[i], $"{path}[{i}]", ref i)
                )
            );
        }

        [Fact]
        public void ForFor()
        {
            var test = new KeyValuePair<int, KeyValuePair<int, string>>(1337, new KeyValuePair<int, string>(1338, "test"));
            var path = $"{nameof(test)}.{nameof(test.Value)}.{nameof(test.Value.Key)}";

            ValidateArgument.For(test, nameof(test), v => v
                .For(x => x.Value, v => v
                    .For(x => x.Key, v => v
                        .AssertEqualValueAndPath(test.Value.Key, path)
                    )
                )
            );
        }

        [Fact]
        public void ForForEach()
        {
            var test = new KeyValuePair<int, List<int>>(1337, new List<int> { 1, 2, 3 });
            var path = $"{nameof(test)}.{nameof(test.Value)}";

            var i = 0;

            ValidateArgument.For(test, nameof(test), v => v
                .For(x => x.Value, v => v
                    .ForEach(x => x, v => v
                        .AssertEqualValueAndPathIndexed(test.Value[i], $"{path}[{i}]", ref i)
                    )
                )
            );
        }

        [Fact]
        public void ForEachFor()
        {
            var test = new List<KeyValuePair<int, string>> { new(1337, "test") };

            var i = 0;

            ValidateArgument.For(test, nameof(test), v => v
                .ForEach(x => x, v => v
                    .For(x => x.Key, v => v
                        .AssertEqualValueAndPathIndexed(test[i].Key,
                            $"{nameof(test)}[{i}].{nameof(KeyValuePair<int, string>.Key)}", ref i)
                    )
                )
            );
        }

        [Fact]
        public void ForEachProperty()
        {
            var test = new KeyValuePair<int, List<int>>(1337, new List<int> { 1, 2, 3 });
            var path = $"{nameof(test)}.{nameof(test.Value)}";

            var i = 0;

            ValidateArgument.For(test, nameof(test), v => v
                .ForEach(x => x.Value, v => v
                    .AssertEqualValueAndPathIndexed(test.Value[i], $"{path}[{i}]", ref i)
                )
            );
        }

        [Fact]
        public void AsEnumerable()
        {
            var test = new List<int> { 1, 2, 3 };

            ValidateArgument.For(test, nameof(test), v => v
                .AsEnumerable(x => x, v => v
                    .NotEmpty()
                )
            );
        }

        [Fact]
        public void AsEnumerableProperty()
        {
            var test = new { List = new List<int> { 1, 2, 3 } };

            ValidateArgument.For(test, nameof(test), v => v
                .AsEnumerable(x => x.List, v => v
                    .NotEmpty()
                )
            );
        }

        [Fact]
        public void ReferenceFor()
        {
            var test = new KeyValuePair<int, string>(1337, "test");
            var path = $"{nameof(test)}.{nameof(test.Key)}";

            var reference = ArgumentReference.For(test, nameof(test))
                .For(x => x.Key);
            reference.AssertEqualValueAndPath(test.Key, path);
        }

        [Fact]
        public void ReferenceForRequiresMember()
        {
            var test = new KeyValuePair<int, string>(1337, "test");

            Assert.Throws<ArgumentException>(() =>
            {
                ArgumentReference.For(test, nameof(test)).For(x => x.ToString());
            });
        }
    }
}