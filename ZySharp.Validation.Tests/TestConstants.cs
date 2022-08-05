using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ZySharp.Validation.Tests
{
    internal static class TestConstants
    {
        [ExcludeFromCodeCoverage]
        public class Obj :
            IComparable<Obj>
        {
            public static readonly Obj A = new() { _v = 1 };
            public static readonly Obj B = new() { _v = 2 };
            public static readonly Obj C = new() { _v = 3 };

            private int _v;

            public int CompareTo(Obj? other)
            {
                return Comparer<int?>.Default.Compare(_v, other?._v);
            }
        }

        public static readonly Guid GuidA = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public static readonly Guid GuidB = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public static readonly Guid GuidC = Guid.Parse("00000000-0000-0000-0000-000000000003");

        public static readonly TimeSpan TimeSpanA = TimeSpan.FromSeconds(1);
        public static readonly TimeSpan TimeSpanB = TimeSpan.FromSeconds(2);
        public static readonly TimeSpan TimeSpanC = TimeSpan.FromSeconds(3);
    }
}