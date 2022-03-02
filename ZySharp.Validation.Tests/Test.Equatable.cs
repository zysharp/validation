using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed partial class Test
    {
        #region Equal

        public static IEnumerable<object[]> DataEqualRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object>(null  , null  , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.A , Obj.A , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.A , Obj.B , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "A"   , false) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "B"   , true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualRef))]
        public void Test_Equal_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataEqualRef))]
        public void Test_Equal_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataEqualVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(0        , 0        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 0        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(0        , 1        , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidA    , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidA    , GuidB    , true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanA, TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanA, TimeSpanB, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualVal))]
        public void Test_Equal_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataEqualVal))]
        public void Test_Equal_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataEqualRefMultiple => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object[]>(null  , new object[]{ Obj.A, null   }, false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.A , new       []{ Obj.B, Obj.A  }, false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.A , new       []{ Obj.B         }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]>("A"   , new       []{ "B", "A"      }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]>("A"   , new       []{ "B", "C", "D" }, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualRefMultiple))]
        public void Test_Equal_Ref_Multiple<T>(RefTestCaseWithParam<T, T[]> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        public static IEnumerable<object[]> DataEqualValMultiple => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0        , new []{ 1, 0                 }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(null     , new []{ 0                    }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0        , new []{ 1                    }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidA    , new []{ GuidB, GuidA         }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(null     , new []{ GuidA                }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidA    , new []{ GuidB                }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanA, new []{ TimeSpanB, TimeSpanA }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(null     , new []{ TimeSpanA            }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanA, new []{ TimeSpanB            }, true ) }
        };

        [Theory]
        [MemberData(nameof(DataEqualValMultiple))]
        public void Test_Equal_Val_Multiple<T>(ValTestCaseWithParam<T, T[]> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.Equal(test.Parameter));
        }

        #endregion Equal

        #region NotEqual

        public static IEnumerable<object[]> DataNotEqualRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object>(null  , null  , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.A , Obj.A , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.A , Obj.B , false) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "A"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("A"   , "B"   , false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualRef))]
        public void Test_NotEqual_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataNotEqualRef))]
        public void Test_NotEqual_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataNotEqualVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(0        , 0        , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 0        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(0        , 1        , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidA    , GuidA    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidA    , GuidB    , false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanA, TimeSpanA, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanA, TimeSpanB, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualVal))]
        public void Test_NotEqual_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataNotEqualVal))]
        public void Test_NotEqual_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataNotEqualRefMultiple => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<object, object[]>(null  , new object[]{ Obj.A, null   }, true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.A , new       []{ Obj.B, Obj.A  }, true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.A , new       []{ Obj.B         }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]>("A"   , new       []{ "B", "A"      }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]>("A"   , new       []{ "B", "C", "D" }, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualRefMultiple))]
        public void Test_NotEqual_Ref_Multiple<T>(RefTestCaseWithParam<T, T[]> test)
            where T : class
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        public static IEnumerable<object[]> DataNotEqualValMultiple => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0        , new []{ 1, 0                 }, true ) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(null     , new []{ 0                    }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(0        , new []{ 1                    }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidA    , new []{ GuidB, GuidA         }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(null     , new []{ GuidA                }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidA    , new []{ GuidB                }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanA, new []{ TimeSpanB, TimeSpanA }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(null     , new []{ TimeSpanA            }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanA, new []{ TimeSpanB            }, false) }
        };

        [Theory]
        [MemberData(nameof(DataNotEqualValMultiple))]
        public void Test_NotEqual_Val_Multiple<T>(ValTestCaseWithParam<T, T[]> test)
            where T : struct
        {
            TestExtensions.TestValidation(test, v => v.NotEqual(test.Parameter));
        }

        #endregion NotEqual
    }
}