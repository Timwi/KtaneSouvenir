using System;

namespace Souvenir;

/// <summary>Used when a Souvenir module processor encounters a condition that requires a module to be abandoned.</summary>
internal sealed class AbandonModuleException : Exception
{
    public AbandonModuleException(string message) : base(message) { }
}
