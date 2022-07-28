using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed class TestEquatable
    {
        #region Equal

        public static IEnumerable<object[]> DataEqualRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object                      >(null               , null                , false) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.A, TestConstants.Obj.A , false) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.A, TestConstants.Obj.B , true ) },
            new object[] { new RefTestCaseWithParam<string, string                      >("A"                , "A"                 , false) },
            new object[] { new RefTestCaseWithParam<string, string                      >("A"                , "B"                 , true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualRef))]
        public void EqualRef<T>([NotNull] RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataEqualRef))]
        public void EqualRefArgumentReference<T>([NotNull] RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataEqualVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(0                      , 0                      , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 0                      , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(0                      , 1                      , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidB    , true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanB, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualVal))]
        public void EqualVal<T>([NotNull] ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataEqualVal))]
        public void EqualValArgumentReference<T>([NotNull] ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataEqualRefMultiple => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object?[]                     >(null               , new object?[]{ TestConstants.Obj.A, null                }, false) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.A, new []       { TestConstants.Obj.B, TestConstants.Obj.A }, false) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.A, new []       { TestConstants.Obj.B                      }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]                      >("A"                , new []       { "B", "A"                                 }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]                      >("A"                , new []       { "B", "C", "D"                            }, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualRefMultiple))]
        public void EqualRefMultiple<T>([NotNull] RefTestCaseWithParam<T, T[]> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter!));
        }

        public static IEnumerable<object[]> DataEqualValMultiple => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0                      , new []{ 1, 0                                             }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(null                   , new []{ 0                                                }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0                      , new []{ 1                                                }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidA    , new []{ TestConstants.GuidB, TestConstants.GuidA         }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(null                   , new []{ TestConstants.GuidA                              }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidA    , new []{ TestConstants.GuidB                              }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanA, new []{ TestConstants.TimeSpanB, TestConstants.TimeSpanA }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(null                   , new []{ TestConstants.TimeSpanA                          }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanA, new []{ TestConstants.TimeSpanB                          }, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualValMultiple))]
        public void EqualValMultiple<T>([NotNull] ValTestCaseWithParam<T, T[]> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter!));
        }

        #endregion Equal

        #region NotEqual

        public static IEnumerable<object[]> DataNotEqualRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object>(null  , null  , true ) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj   , TestConstants.Obj   >(TestConstants.Obj.A , TestConstants.Obj.A , true ) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj   , TestConstants.Obj   >(TestConstants.Obj.A , TestConstants.Obj.B , false) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "A"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "B"   , false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualRef))]
        public void NotEqualRef<T>([NotNull] RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataNotEqualRef))]
        public void NotEqualRefArgumentReference<T>([NotNull] RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataNotEqualVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(0                      , 0                      , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 0                      , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(0                      , 1                      , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidA    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidB    , false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanA, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanB, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualVal))]
        public void NotEqualVal<T>([NotNull] ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataNotEqualVal))]
        public void NotEqualValArgumentReference<T>([NotNull] ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataNotEqualRefMultiple => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object?[]                     >(null               , new object?[]{ TestConstants.Obj.A, null                }, true ) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.A, new        []{ TestConstants.Obj.B, TestConstants.Obj.A }, true ) },
            new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.A, new        []{ TestConstants.Obj.B                      }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]                      >("A"                , new        []{ "B", "A"                                 }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]                      >("A"                , new        []{ "B", "C", "D"                            }, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualRefMultiple))]
        public void NotEqualRefMultiple<T>([NotNull] RefTestCaseWithParam<T, T[]> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter!));
        }

        public static IEnumerable<object[]> DataNotEqualValMultiple => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0                      , new []{ 1, 0                                             }, true ) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(null                   , new []{ 0                                                }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0                      , new []{ 1                                                }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidA    , new []{ TestConstants.GuidB, TestConstants.GuidA         }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(null                   , new []{ TestConstants.GuidA                              }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidA    , new []{ TestConstants.GuidB                              }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanA, new []{ TestConstants.TimeSpanB, TestConstants.TimeSpanA }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(null                   , new []{ TestConstants.TimeSpanA                          }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanA, new []{ TestConstants.TimeSpanB                          }, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualValMultiple))]
        public void NotEqualValMultiple<T>([NotNull] ValTestCaseWithParam<T, T[]> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter!));
        }

        #endregion NotEqual
    }
}