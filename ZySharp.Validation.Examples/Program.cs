using System;
using System.Collections.Generic;
using System.Reflection;

namespace ZySharp.Validation.Examples
{
    internal class Program
    {
        private static void ValidateProperty(Assembly assembly)
        {
            ValidateArgument.For(assembly, nameof(assembly), v => v
                .NotNull()                      // <- The argument must not be `null`
                .For(x => x.Location, v => v    // <- Validate property
                    .NotNullOrEmpty()           // <- The `Location` property must not be `null` or an empty string
                )
            );
        }

        private static void ValidateEnumerable(IReadOnlyCollection<Guid> ids)
        {
            ValidateArgument.For(ids, nameof(ids), v => v
                .NotNullOrEmpty()               // <- The enumerable must not be `null` and must contain at least one element
                .ForEach(x => x, v => v         // <- Validate all elements in the enumerable
                    .NotEmpty()                 // <- Each individual element in the enumerable must not be empty an empty GUID
                )
            );
        }

        private static void ValidateNullable(int? value)
        {
            // Validation is implicitly executed if value is not `null`

            ValidateArgument.For(value, nameof(value), v => v
                .NotEmpty()
            );

            // .. which is equivalent to the following manual validation

            ValidateArgument.For(value, nameof(value), v => v
                .When(x => x.HasValue, v => v
                    .Select(x => x.Value, v => v
                        .NotEmpty()
                    )
                )
            );
        }

        private static void ValidateReference(DateTime start, DateTime end)
        {
            ValidateArgument.For(start, nameof(start), v => v
                .LessThan(ArgumentReference.For(end, nameof(end)))
            );
        }

        private static void Main(string[] args)
        {
            ValidateProperty(Assembly.GetExecutingAssembly());
            ValidateEnumerable(new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() });
            ValidateNullable(1337);
            ValidateReference(DateTime.UtcNow, DateTime.MaxValue);
        }
    }
}