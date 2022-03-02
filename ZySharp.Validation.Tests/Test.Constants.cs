using System;

namespace ZySharp.Validation.Tests
{
    public sealed partial class Test
    {
        private sealed class Obj :
            IComparable<Obj>
        {
            public static readonly Obj A = new Obj { _v = 1 };
            public static readonly Obj B = new Obj { _v = 2 };
            public static readonly Obj C = new Obj { _v = 3 };

            private int _v;

            public int CompareTo(Obj other)
            {
                return (_v - other._v);
            }
        }

        private static readonly Guid GuidA = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private static readonly Guid GuidB = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private static readonly Guid GuidC = Guid.Parse("00000000-0000-0000-0000-000000000003");

        private static readonly TimeSpan TimeSpanA = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan TimeSpanB = TimeSpan.FromSeconds(2);
        private static readonly TimeSpan TimeSpanC = TimeSpan.FromSeconds(3);
    }
}