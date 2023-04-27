using System;
using System.Collections.Generic;

using Xunit;

namespace ZySharp.Validation.Tests;

[Trait("Category", "Unit")]
public sealed class TestBasic
{
    #region NotNull

    public static IEnumerable<object[]> DataNotNullRef => new List<object[]>
    {
        new object[] { new RefTestCase<object           >(new object()       , false                               ) },
        new object[] { new RefTestCase<string           >(string.Empty       , false                               ) },
        new object[] { new RefTestCase<string           >("test"             , false                               ) },
        new object[] { new RefTestCase<TestConstants.Obj>(TestConstants.Obj.A, false                               ) },
        new object[] { new RefTestCase<object           >(null               , true , typeof(ArgumentNullException)) }
    };

    [Theory]
    [MemberData(nameof(DataNotNullRef))]
    public void NotNullRef<T>(RefTestCase<T> test)
        where T : class
    {
        TestExtensions.TestValidation(test, v => v.NotNull());
    }

    public static IEnumerable<object[]> DataNotNullVal => new List<object[]>
    {
        new object[] { new ValTestCase<int     >(0            , false                               ) },
        new object[] { new ValTestCase<int     >(null         , true , typeof(ArgumentNullException)) },
        new object[] { new ValTestCase<Guid    >(Guid.Empty   , false                               ) },
        new object[] { new ValTestCase<Guid    >(null         , true , typeof(ArgumentNullException)) },
        new object[] { new ValTestCase<TimeSpan>(TimeSpan.Zero, false                               ) },
        new object[] { new ValTestCase<TimeSpan>(null         , true , typeof(ArgumentNullException)) }
    };

    [Theory]
    [MemberData(nameof(DataNotNullVal))]
    public void NotNullVal<T>(ValTestCase<T> test)
        where T : struct
    {
        TestExtensions.TestValidation(test, v => v.NotNull());
    }

    #endregion NotNull

    #region NotEmpty

    public static IEnumerable<object[]> DataNotEmptyVal => new List<object[]>
    {
        new object[] { new ValTestCase<int     >(1                      , false) },
        new object[] { new ValTestCase<int     >(null                   , false) },
        new object[] { new ValTestCase<int     >(0                      , true ) },
        new object[] { new ValTestCase<Guid    >(TestConstants.GuidA    , false) },
        new object[] { new ValTestCase<Guid    >(null                   , false) },
        new object[] { new ValTestCase<Guid    >(Guid.Empty             , true ) },
        new object[] { new ValTestCase<TimeSpan>(TestConstants.TimeSpanA, false) },
        new object[] { new ValTestCase<TimeSpan>(null                   , false) },
        new object[] { new ValTestCase<TimeSpan>(TimeSpan.Zero          , true ) },
    };

    [Theory]
    [MemberData(nameof(DataNotEmptyVal))]
    public void NotEmptyVal<T>(ValTestCase<T> test)
        where T : struct
    {
        TestExtensions.TestValidation(test, v => v.NotEmpty());
    }

    public static IEnumerable<object[]> DataNotEmptyEnumerable => new List<object[]>
    {
        new object[] { new RefTestCase<IEnumerable<int>>(new List<int>{ 1 }, false) },
        new object[] { new RefTestCase<IEnumerable<int>>(null              , false) },
        new object[] { new RefTestCase<IEnumerable<int>>(new List<int>()   , true ) },
    };

    [Theory]
    [MemberData(nameof(DataNotEmptyEnumerable))]
    public void NotEmptyEnumerable<T>(RefTestCase<IEnumerable<T>> test)
    {
        TestExtensions.TestValidation(test.Value, v => v.NotEmpty(), test.ExpectThrow);
    }

    public static IEnumerable<object[]> DataNotEmptyString => new List<object[]>
    {
        new object[] { new RefTestCase<string>("test"      , false) },
        new object[] { new RefTestCase<string>(null        , false) },
        new object[] { new RefTestCase<string>(string.Empty, true ) },
        new object[] { new RefTestCase<string>(""          , true ) },
    };

    [Theory]
    [MemberData(nameof(DataNotEmptyString))]
    public void NotEmptyString(RefTestCase<string> test)
    {
        TestExtensions.TestValidation(test.Value, v => v.NotEmpty(), test.ExpectThrow);
    }

    #endregion NotEmpty

    #region NotNullOrEmpty

    public static IEnumerable<object[]> DataNotNullOrEmptyRef => new List<object[]>
    {
        new object[] { new RefTestCase<string   >("test"            , false                               ) },
        new object[] { new RefTestCase<string   >(string.Empty      , true                                ) },
        new object[] { new RefTestCase<string   >(null              , true , typeof(ArgumentNullException)) },
        new object[] { new RefTestCase<List<int>>(new List<int>{ 1 }, false                               ) },
        new object[] { new RefTestCase<List<int>>(new List<int>()   , true                                ) },
        new object[] { new RefTestCase<List<int>>(null              , true , typeof(ArgumentNullException)) }
    };

    [Theory]
    [MemberData(nameof(DataNotNullOrEmptyRef))]
    public void NotNullOrEmptyRef<T>(RefTestCase<T> test)
        where T : class
    {
        TestExtensions.TestValidation(test, v => v.NotNullOrEmpty());
    }

    public static IEnumerable<object[]> DataNotNullOrEmptyVal => new List<object[]>
    {
        new object[] { new ValTestCase<int     >(1                      , false                               ) },
        new object[] { new ValTestCase<int     >(0                      , true                                ) },
        new object[] { new ValTestCase<int     >(null                   , true , typeof(ArgumentNullException)) },
        new object[] { new ValTestCase<Guid    >(TestConstants.GuidA    , false                               ) },
        new object[] { new ValTestCase<Guid    >(Guid.Empty             , true                                ) },
        new object[] { new ValTestCase<Guid    >(null                   , true , typeof(ArgumentNullException)) },
        new object[] { new ValTestCase<TimeSpan>(TestConstants.TimeSpanA, false                               ) },
        new object[] { new ValTestCase<TimeSpan>(TimeSpan.Zero          , true                                ) },
        new object[] { new ValTestCase<TimeSpan>(null                   , true , typeof(ArgumentNullException)) }
    };

    [Theory]
    [MemberData(nameof(DataNotNullOrEmptyVal))]
    public void NotNullOrEmptyVal<T>(ValTestCase<T> test)
        where T : struct
    {
        TestExtensions.TestValidation(test, v => v.NotNullOrEmpty());
    }

    #endregion NotNullOrEmpty

    #region MutuallyExclusive

    public static IEnumerable<object[]> DataMutuallyExclusiveRef => new List<object[]>
    {
        new object[] { new RefTestCaseWithParam<object, object                      >(null               , null                , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.A, TestConstants.Obj.A , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.A, TestConstants.Obj.B , true ) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(TestConstants.Obj.A, null                , false) },
        new object[] { new RefTestCaseWithParam<TestConstants.Obj, TestConstants.Obj>(null               , TestConstants.Obj.B , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >("A"                , "A"                 , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("A"                , "B"                 , true ) },
        new object[] { new RefTestCaseWithParam<string, string                      >("A"                , null                , false) },
        new object[] { new RefTestCaseWithParam<string, string                      >(null               , "B"                 , false) }
    };

    [Theory]
    [MemberData(nameof(DataMutuallyExclusiveRef))]
    public void MutuallyExclusiveRef<T>(RefTestCaseWithParam<T, T> test)
        where T : class
    {
        TestExtensions.TestValidation(test, v => v.MutuallyExclusive(ArgumentReference.For(test.Parameter, "param")));
    }

    public static IEnumerable<object[]> DataMutuallyExclusiveVal => new List<object[]>
    {
        new object[] { new ValTestCaseWithValParam<int     , int     >(null                   , null                   , false) },
        new object[] { new ValTestCaseWithValParam<int     , int     >(0                      , 0                      , true ) },
        new object[] { new ValTestCaseWithValParam<int     , int     >(0                      , 1                      , true ) },
        new object[] { new ValTestCaseWithValParam<int     , int     >(0                      , null                   , false) },
        new object[] { new ValTestCaseWithValParam<int     , int     >(null                   , 1                      , false) },
        new object[] { new ValTestCaseWithValParam<Guid    , Guid    >(null                   , null                   , false) },
        new object[] { new ValTestCaseWithValParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidA    , true ) },
        new object[] { new ValTestCaseWithValParam<Guid    , Guid    >(TestConstants.GuidA    , TestConstants.GuidB    , true ) },
        new object[] { new ValTestCaseWithValParam<Guid    , Guid    >(TestConstants.GuidA    , null                   , false) },
        new object[] { new ValTestCaseWithValParam<Guid    , Guid    >(null                   , TestConstants.GuidB    , false) },
        new object[] { new ValTestCaseWithValParam<TimeSpan, TimeSpan>(null                   , null                   , false) },
        new object[] { new ValTestCaseWithValParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanA, true ) },
        new object[] { new ValTestCaseWithValParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, TestConstants.TimeSpanB, true ) },
        new object[] { new ValTestCaseWithValParam<TimeSpan, TimeSpan>(TestConstants.TimeSpanA, null                   , false) },
        new object[] { new ValTestCaseWithValParam<TimeSpan, TimeSpan>(null                   , TestConstants.TimeSpanB, false) }
    };

    [Theory]
    [MemberData(nameof(DataMutuallyExclusiveVal))]
    public void MutuallyExclusiveVal<T>(ValTestCaseWithValParam<T, T> test)
        where T : struct
    {
        TestExtensions.TestValidation(test, v => v.MutuallyExclusive(ArgumentReference.For(test.Parameter, "param")));
    }

    #endregion MutuallyExclusive
}
