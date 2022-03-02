using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests
{
    public sealed partial class Test
    {
        #region GreaterThan

        public static IEnumerable<object[]> DataGreaterThanRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.A , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.B , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.C , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "A"   , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "B"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "C"   , true ) }
        };

        [Theory]
        [MemberData(nameof(DataGreaterThanRef))]
        public void Test_GreaterThan_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThan(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataGreaterThanRef))]
        public void Test_GreaterThan_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThan(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataGreaterThanVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 2        , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 3        , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidB    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidC    , true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanB, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanC, true ) }
        };

        [Theory]
        [MemberData(nameof(DataGreaterThanVal))]
        public void Test_GreaterThan_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThan(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataGreaterThanVal))]
        public void Test_GreaterThan_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThan(ArgumentReference.For(test.Parameter, "param")));
        }

        #endregion GreaterThan

        #region GreaterThanOrEqualTo

        public static IEnumerable<object[]> DataGreaterThanOrEqualToRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.A , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.B , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.C , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "A"   , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "B"   , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "C"   , true ) }
        };

        [Theory]
        [MemberData(nameof(DataGreaterThanOrEqualToRef))]
        public void Test_GreaterThanOrEqualTo_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataGreaterThanOrEqualToRef))]
        public void Test_GreaterThanOrEqualTo_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataGreaterThanOrEqualToVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 2        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 3        , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidB    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidC    , true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanB, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanC, true ) }
        };

        [Theory]
        [MemberData(nameof(DataGreaterThanOrEqualToVal))]
        public void Test_GreaterThanOrEqualTo_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataGreaterThanOrEqualToVal))]
        public void Test_GreaterThanOrEqualTo_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
        }

        #endregion GreaterThanOrEqualTo

        #region LessThan

        public static IEnumerable<object[]> DataLessThanRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.A , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.B , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.C , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "A"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "B"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "C"   , false) }
        };

        [Theory]
        [MemberData(nameof(DataLessThanRef))]
        public void Test_LessThan_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThan(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataLessThanRef))]
        public void Test_LessThan_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThan(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataLessThanVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 1        , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 2        , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 3        , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidA    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidB    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidC    , false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanA, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanB, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanC, false) }
        };

        [Theory]
        [MemberData(nameof(DataLessThanVal))]
        public void Test_LessThan_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThan(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataLessThanVal))]
        public void Test_LessThan_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThan(ArgumentReference.For(test.Parameter, "param")));
        }

        #endregion LessThan

        #region LessThanOrEqualTo

        public static IEnumerable<object[]> DataLessThanOrEqualToRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.A , true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.B , false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj   >(Obj.B , Obj.C , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "A"   , true ) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "B"   , false) },
            new object[] { new RefTestCaseWithParam<string, string>("B"   , "C"   , false) }
        };

        [Theory]
        [MemberData(nameof(DataLessThanOrEqualToRef))]
        public void Test_LessThanOrEqualTo_Ref<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataLessThanOrEqualToRef))]
        public void Test_LessThanOrEqualTo_Ref_ArgumentReference<T>(RefTestCaseWithParam<T, T> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
        }

        public static IEnumerable<object[]> DataLessThanOrEqualToVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int     >(null     , 1        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 1        , true ) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 2        , false) },
            new object[] { new ValTestCaseWithParam<int     , int     >(2        , 3        , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null     , GuidA    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidA    , true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidB    , false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid    >(GuidB    , GuidC    , false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null     , TimeSpanA, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanA, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanB, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TimeSpanB, TimeSpanC, false) }
        };

        [Theory]
        [MemberData(nameof(DataLessThanOrEqualToVal))]
        public void Test_LessThanOrEqualTo_Val<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(test.Parameter));
        }

        [Theory]
        [MemberData(nameof(DataLessThanOrEqualToVal))]
        public void Test_LessThanOrEqualTo_Val_ArgumentReference<T>(ValTestCaseWithParam<T, T> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
        }

        #endregion LessThanOrEqualTo

        #region InRange

        public static IEnumerable<object[]> DataInRangeRef => new List<object[]>
        {
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.B , new[] { Obj.A, Obj.C }, false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.B , new[] { Obj.B, Obj.C }, false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.B , new[] { Obj.A, Obj.B }, false) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.B , new[] { Obj.A, Obj.A }, true ) },
            new object[] { new RefTestCaseWithParam<Obj   , Obj[]   >(Obj.B , new[] { Obj.C, Obj.C }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]>("B"   , new[] { "A", "C"     }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]>("B"   , new[] { "B", "C"     }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]>("B"   , new[] { "A", "B"     }, false) },
            new object[] { new RefTestCaseWithParam<string, string[]>("B"   , new[] { "A", "A"     }, true ) },
            new object[] { new RefTestCaseWithParam<string, string[]>("B"   , new[] { "C", "C"     }, true ) },
        };

        [Theory]
        [MemberData(nameof(DataInRangeRef))]
        public void Test_InRange_Ref<T>(RefTestCaseWithParam<T, T[]> test)
            where T : class, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.InRange(test.Parameter[0], test.Parameter[1]));
        }

        public static IEnumerable<object[]> DataInRangeVal => new List<object[]>
        {
            new object[] { new ValTestCaseWithParam<int     , int[]     >(2        , new[] { 1        , 3         }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(2        , new[] { 2        , 3         }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(2        , new[] { 1        , 2         }, false) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(2        , new[] { 1        , 1         }, true ) },
            new object[] { new ValTestCaseWithParam<int     , int[]     >(2        , new[] { 3        , 3         }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidB    , new[] { GuidA    , GuidC     }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidB    , new[] { GuidB    , GuidC     }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidB    , new[] { GuidA    , GuidB     }, false) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidB    , new[] { GuidA    , GuidA     }, true ) },
            new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(GuidB    , new[] { GuidC    , GuidC     }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanB, new[] { TimeSpanA, TimeSpanC }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanB, new[] { TimeSpanB, TimeSpanC }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanB, new[] { TimeSpanA, TimeSpanB }, false) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanB, new[] { TimeSpanB, TimeSpanA }, true ) },
            new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TimeSpanB, new[] { TimeSpanC, TimeSpanC }, true ) }
        };

        [Theory]
        [MemberData(nameof(DataInRangeVal))]
        public void Test_InRange_Val<T>(ValTestCaseWithParam<T, T[]> test)
            where T : struct, IComparable<T>
        {
            TestExtensions.TestValidation(test, v => v.InRange(test.Parameter[0], test.Parameter[1]));
        }

        #endregion InRange
    }
}