using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ZySharp.Validation
{
    internal sealed class ValidatorContext<T> :
        IValidatorContext<T>,
        IArgumentReference<T>
    {
        public IList<string> Path { get; internal set; }

        public T Value { get; internal set; }

        public Exception Exception { get; internal set; }

        internal ValidatorContext(T value, string name)
        {
            Value = value;
            Path = new List<string> { name };
        }

        internal ValidatorContext(T value, IEnumerable<string> path, string name)
        {
            Value = value;
            Path = new List<string>(path);

            if (name is not null)
            {
                Path.Add(name);
            }
        }

        void IValidatorContext<T>.SetArgumentException(string message)
        {
            Contract.Assert(!string.IsNullOrEmpty(message));

            Exception = new ArgumentException(message, Path.First());
        }

        void IValidatorContext<T>.SetArgumentNullException(string message)
        {
            Contract.Assert(!string.IsNullOrEmpty(message));

            Exception = new ArgumentNullException(Path.First(), message);
        }

        void IValidatorContext<T>.SetForeignException<TOther>(IValidatorContext<TOther> validator)
        {
            Contract.Assert(validator is not null);

            Exception = validator.Exception;
        }
    }
}