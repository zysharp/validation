using System.Collections.Generic;

namespace ZySharp.Validation
{
    /// <summary>
    /// Represents a validator context that is used to provide fluent-api functionality.
    /// </summary>
    /// <typeparam name="T">The current value.</typeparam>
    public interface IValidatorContext<out T>
    {
        /// <summary>
        /// The current path.
        /// </summary>
        public IList<string> Path { get;  }

        /// <summary>
        /// The current value.
        /// </summary>
        public T Value { get;  }
    }
}