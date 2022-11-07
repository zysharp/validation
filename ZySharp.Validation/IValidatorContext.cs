using System;
using System.Collections.Generic;

namespace ZySharp.Validation;

/// <summary>
/// Represents a validator context that is used to provide fluent-api functionality.
/// </summary>
/// <typeparam name="T">The current value.</typeparam>
public interface IValidatorContext<out T>
{
    /// <summary>
    /// The current path.
    /// </summary>
    public IList<string> Path { get; }

    /// <summary>
    /// The current value.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// The current exception object.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// Assigns a new <see cref="ArgumentException"/> to the <see cref="Exception"/> property.
    /// </summary>
    /// <param name="message">The exception message.</param>
    internal void SetArgumentException(string message);

    /// <summary>
    /// Assigns a new <see cref="ArgumentNullException"/> to the <see cref="Exception"/> property.
    /// </summary>
    /// <param name="message">The exception message.</param>
    internal void SetArgumentNullException(string message);

    /// <summary>
    /// Assigns the current exception of another <see cref="IValidatorContext{TOther}"/> instance
    /// to the <see cref="Exception"/> property.
    /// </summary>
    /// <param name="validator">The other <see cref="IValidatorContext{TOther}"/> instance.</param>
    internal void SetForeignException<TOther>(IValidatorContext<TOther> validator);
}
