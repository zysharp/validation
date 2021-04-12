using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestSelectors
    {
        [Fact]
        public void Test_For()
        {
            var test = new KeyValuePair<int, string>(1337, "test");
            var path = nameof(test);

            ValidateArgument
                .For(test, nameof(test))
                .AssertEqualValueAndPath(test, path);
        }

        [Fact]
        public void Test_Selector_For()
        {
            var test = new KeyValuePair<int, string>(1337, "test");
            var path = $"{nameof(test)}.{nameof(test.Key)}";

            ValidateArgument
                .For(test, nameof(test))
                .For(x => x.Key, v => v
                    .AssertEqualValueAndPath(test.Key, path)
                );
        }

        [Fact]
        public void Test_Selector_For_For()
        {
            var test = new KeyValuePair<int, KeyValuePair<int, string>>(1337, new KeyValuePair<int, string>(1338, "test"));
            var path = $"{nameof(test)}.{nameof(test.Value)}.{nameof(test.Value.Key)}";

            ValidateArgument
                .For(test, nameof(test))
                .For(x => x.Value, v => v
                    .For(x => x.Key, v => v
                        .AssertEqualValueAndPath(test.Value.Key, path)
                    )
                );
        }

        [Fact]
        public void Test_Selector_For_ForEach()
        {
            var test = new KeyValuePair<int, List<int>>(1337, new List<int> { 1, 2, 3 });
            var path = $"{nameof(test)}.{nameof(test.Value)}";

            var i = 0;

            ValidateArgument
                .For(test, nameof(test))
                .For(x => x.Value, v => v
                    .ForEach(x => x, v => v
                        .AssertEqualValueAndPathIndexed(test.Value[i], $"{path}[{i}]", ref i)
                    )
                );
        }

        [Fact]
        public void Test_Selector_ForEach_Parameter()
        {
            var test = new List<int> { 1, 2, 3 };
            var path = nameof(test);

            var i = 0;

            ValidateArgument
                .For(test, nameof(test))
                .ForEach(x => x, v => v
                    .AssertEqualValueAndPathIndexed(test[i], $"{path}[{i}]", ref i)
                );
        }

        [Fact]
        public void Test_Selector_ForEach_Property()
        {
            var test = new KeyValuePair<int, List<int>>(1337, new List<int> { 1, 2, 3 });
            var path = $"{nameof(test)}.{nameof(test.Value)}";

            var i = 0;

            ValidateArgument
                .For(test, nameof(test))
                .ForEach(x => x.Value, v => v
                    .AssertEqualValueAndPathIndexed(test.Value[i], $"{path}[{i}]", ref i)
                );
        }

        [Fact]
        public void Test_Selector_ForEach_For()
        {
            var test = new List<KeyValuePair<int, string>> { new (1337, "test") };

            var i = 0;

            ValidateArgument
                .For(test, nameof(test))
                .ForEach(x => x, v => v
                    .For(x => x.Key, v => v
                        .AssertEqualValueAndPathIndexed(test[i].Key,
                            $"{nameof(test)}[{i}].{nameof(KeyValuePair<int, string>.Key)}", ref i)
                    )
                );
        }
    }
}