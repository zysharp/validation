using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using Xunit;

namespace ZySharp.Validation.Tests
{
    [Trait("Category", "Unit")]
    public sealed class TestStream
    {
        [Fact]
        public void IsReadable()
        {
            using var s1 = new DummyStream(canRead: true);

            ValidateArgument.For(s1, nameof(s1), v => v.IsReadable());

            using var s2 = new DummyStream(canRead: false);

            Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument.For(s2, nameof(s2), v => v.IsReadable());
            });
        }

        [Fact]
        public void IsWritable()
        {
            using var s1 = new DummyStream(canWrite: true);

            ValidateArgument.For(s1, nameof(s1), v => v.IsWritable());

            using var s2 = new DummyStream(canWrite: false);

            Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument.For(s2, nameof(s2), v => v.IsWritable());
            });
        }

        [Fact]
        public void IsSeekable()
        {
            using var s1 = new DummyStream(canSeek: true);

            ValidateArgument.For(s1, nameof(s1), v => v.IsSeekable());

            using var s2 = new DummyStream(canSeek: false);

            Assert.Throws<ArgumentException>(() =>
            {
                ValidateArgument.For(s2, nameof(s2), v => v.IsSeekable());
            });
        }

        #region Internal

        [ExcludeFromCodeCoverage]
        internal sealed class DummyStream :
            Stream
        {
            private readonly bool _canRead;
            private readonly bool _canWrite;
            private readonly bool _canSeek;

            public DummyStream(bool canRead = true, bool canWrite = true, bool canSeek = true)
            {
                _canRead = canRead;
                _canWrite = canWrite;
                _canSeek = canSeek;
            }

            public override void Flush()
            {
                throw new NotImplementedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotImplementedException();
            }

            public override void SetLength(long value)
            {
                throw new NotImplementedException();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                throw new NotImplementedException();
            }

            public override bool CanRead => _canRead;
            public override bool CanSeek => _canSeek;
            public override bool CanWrite => _canWrite;
            public override long Length { get; }
            public override long Position { get; set; }
        }

        #endregion Internal
    }
}