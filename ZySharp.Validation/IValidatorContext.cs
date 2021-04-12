using System.Collections.Generic;

namespace ZySharp.Validation
{
    public interface IValidatorContext<out T>
    {
        public IList<string> Path { get;  }

        public T Value { get;  }
    }
}