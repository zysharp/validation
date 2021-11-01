using System.Collections.Generic;

namespace ZySharp.Validation
{
    internal sealed class ValidatorContext<T> :
        IValidatorContext<T>,
        IArgumentReference<T>
    {
        public IList<string> Path { get; internal set; }

        public T Value { get; internal set; }

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
    }
}