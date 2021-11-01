using System.Collections.Generic;

namespace ZySharp.Validation
{
    /// <summary>
    /// Represents an argument reference.
    /// </summary>
    /// <typeparam name="T">The current value.</typeparam>
    public interface IArgumentReference<out T>
    {
        /// <summary>
        /// The current path.
        /// </summary>
        public IList<string> Path { get; }

        /// <summary>
        /// The current value.
        /// </summary>
        public T Value { get; }
    }
}