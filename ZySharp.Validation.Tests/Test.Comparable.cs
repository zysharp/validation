using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests;

[Trait("Category", "Unit")]
public sealed class TestComparable
{
    #region GreaterThan

    public static IEnumerable<object[]> DataGreaterThanRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<TestConstants.Obj   , TestConstants.Obj   >(TestConstants.Obj.B , TestConstants.Obj.A , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj   , TestConstants.Obj   >(TestConstants.Obj.B , TestConstants.Obj.B , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj   , TestConstants.Obj   >(TestConstants.Obj.B , TestConstants.Obj.C , true ) },
        new object[] { new RefTestCaseWithParam<string, string>("B"   , "A"   , false) },
        new object[] { new RefTestCaseWithParam<string, string>("B"   , "B"   , true ) },
        new object[] { new RefTestCaseWithParam<string, string>("B"   , "C"   , true ) }
    };

    [Theory]
    [MemberData(nameof(DataGreaterThanRef))]
    public void GreaterThanRef<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThan(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataGreaterThanRef))]
    public void GreaterThanRefArgumentReference<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThan(ArgumentReference.For(test.Parameter, "param")));
    }

    public static IEnumerable<object[]> DataGreaterThanVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 2                      , true ) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 3                      , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidB    , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidC    , true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanB, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanC, true ) }
    };

    [Theory]
    [MemberData(nameof(DataGreaterThanVal))]
    public void GreaterThanVal<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThan(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataGreaterThanVal))]
    public void GreaterThanValArgumentReference<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThan(ArgumentReference.For(test.Parameter, "param")));
    }

    #endregion GreaterThan

    #region GreaterThanOrEqualTo

    public static IEnumerable<object[]> DataGreaterThanOrEqualToRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.A , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.B , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.C , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "A"                 , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "B"                 , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "C"                 , true ) }
    };

    [Theory]
    [MemberData(nameof(DataGreaterThanOrEqualToRef))]
    public void GreaterThanOrEqualToRef<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataGreaterThanOrEqualToRef))]
    public void GreaterThanOrEqualToRefArgumentReference<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
    }

    public static IEnumerable<object[]> DataGreaterThanOrEqualToVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 2                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 3                      , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidB    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidC    , true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanB, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanC, true ) }
    };

    [Theory]
    [MemberData(nameof(DataGreaterThanOrEqualToVal))]
    public void GreaterThanOrEqualToVal<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataGreaterThanOrEqualToVal))]
    public void GreaterThanOrEqualToValArgumentReference<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.GreaterThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
    }

    #endregion GreaterThanOrEqualTo

    #region LessThan

    public static IEnumerable<object[]> DataLessThanRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.A , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.B , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B , TestConstants.Obj.C , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "A"                 , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "B"                 , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                 , "C"                 , false) }
    };

    [Theory]
    [MemberData(nameof(DataLessThanRef))]
    public void LessThanRef<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThan(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataLessThanRef))]
    public void LessThanRefArgumentReference<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThan(ArgumentReference.For(test.Parameter, "param")));
    }

    public static IEnumerable<object[]> DataLessThanVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 1                      , true ) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 2                      , true ) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 3                      , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidA    , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidB    , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidC    , false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanA, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanB, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanC, false) }
    };

    [Theory]
    [MemberData(nameof(DataLessThanVal))]
    public void LessThanVal<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThan(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataLessThanVal))]
    public void LessThanValArgumentReference<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThan(ArgumentReference.For(test.Parameter, "param")));
    }

    #endregion LessThan

    #region LessThanOrEqualTo

    public static IEnumerable<object[]> DataLessThanOrEqualToRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B, TestConstants.Obj.A , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B, TestConstants.Obj.B , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.B, TestConstants.Obj.C , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                , "A"                 , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                , "B"                 , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("B"                , "C"                 , false) }
    };

    [Theory]
    [MemberData(nameof(DataLessThanOrEqualToRef))]
    public void LessThanOrEqualToRef<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataLessThanOrEqualToRef))]
    public void LessThanOrEqualToRefArgumentReference<T>(RefTestCaseWithParam<T, T> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
    }

    public static IEnumerable<object[]> DataLessThanOrEqualToVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithParam<int     , int     >(null                   , 1                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 1                      , true ) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 2                      , false) },
        new object[] { new ValTestCaseWithParam<int     , int     >(2                      , 3                      , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(null                   , TestConstants.GuidA    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidA    , true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidB    , false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid    >(TestConstants.GuidB    , TestConstants.GuidC    , false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanA, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanB, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanB, TestConstants.TimeSpanC, false) }
    };

    [Theory]
    [MemberData(nameof(DataLessThanOrEqualToVal))]
    public void LessThanOrEqualToVal<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(test.Parameter));
    }

    [Theory]
    [MemberData(nameof(DataLessThanOrEqualToVal))]
    public void LessThanOrEqualToValArgumentReference<T>(ValTestCaseWithParam<T, T> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.LessThanOrEqualTo(ArgumentReference.For(test.Parameter, "param")));
    }

    #endregion LessThanOrEqualTo

    #region InRange

    public static IEnumerable<object[]> DataInRangeRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.B, new[] { TestConstants.Obj.A, TestConstants.Obj.C }, false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.B, new[] { TestConstants.Obj.B, TestConstants.Obj.C }, false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.B, new[] { TestConstants.Obj.A, TestConstants.Obj.B }, false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.B, new[] { TestConstants.Obj.A, TestConstants.Obj.A }, true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj[]>(TestConstants.Obj.B, new[] { TestConstants.Obj.C, TestConstants.Obj.C }, true ) },
        new object[] { new RefTestCaseWithParam<string, string[]                      >("B"                , new[] { "A"                , "C"                 }, false) },
        new object[] { new RefTestCaseWithParam<string, string[]                      >("B"                , new[] { "B"                , "C"                 }, false) },
        new object[] { new RefTestCaseWithParam<string, string[]                      >("B"                , new[] { "A"                , "B"                 }, false) },
        new object[] { new RefTestCaseWithParam<string, string[]                      >("B"                , new[] { "A"                , "A"                 }, true ) },
        new object[] { new RefTestCaseWithParam<string, string[]                      >("B"                , new[] { "C"                , "C"                 }, true ) },
    };

    [Theory]
    [MemberData(nameof(DataInRangeRef))]
    public void InRangeRef<T>(RefTestCaseWithParam<T, T[]> test)
        where T : class, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.InRange(test.Parameter![0], test.Parameter![1]));
    }

    public static IEnumerable<object[]> DataInRangeVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithParam<int     , int[]     >(2                      , new[] { 1                      , 3                       }, false) },
        new object[] { new ValTestCaseWithParam<int     , int[]     >(2                      , new[] { 2                      , 3                       }, false) },
        new object[] { new ValTestCaseWithParam<int     , int[]     >(2                      , new[] { 1                      , 2                       }, false) },
        new object[] { new ValTestCaseWithParam<int     , int[]     >(2                      , new[] { 1                      , 1                       }, true ) },
        new object[] { new ValTestCaseWithParam<int     , int[]     >(2                      , new[] { 3                      , 3                       }, true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidB    , new[] { TestConstants.GuidA    , TestConstants.GuidC     }, false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidB    , new[] { TestConstants.GuidB    , TestConstants.GuidC     }, false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidB    , new[] { TestConstants.GuidA    , TestConstants.GuidB     }, false) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidB    , new[] { TestConstants.GuidA    , TestConstants.GuidA     }, true ) },
        new object[] { new ValTestCaseWithParam<Guid    , Guid[]    >(TestConstants.GuidB    , new[] { TestConstants.GuidC    , TestConstants.GuidC     }, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanB, new[] { TestConstants.TimeSpanA, TestConstants.TimeSpanC }, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanB, new[] { TestConstants.TimeSpanB, TestConstants.TimeSpanC }, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanB, new[] { TestConstants.TimeSpanA, TestConstants.TimeSpanB }, false) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanB, new[] { TestConstants.TimeSpanB, TestConstants.TimeSpanA }, true ) },
        new object[] { new ValTestCaseWithParam<TimeSpan, TimeSpan[]>(TestConstants.TimeSpanB, new[] { TestConstants.TimeSpanC, TestConstants.TimeSpanC }, true ) }
    };

    [Theory]
    [MemberData(nameof(DataInRangeVal))]
    public void InRangeVal<T>(ValTestCaseWithParam<T, T[]> test)
        where T : struct, IComparable<T>
    {
        TestExtensions.TestValidation(test, v => v.InRange(test.Parameter![0], test.Parameter![1]));
    }

    #endregion InRange
}
