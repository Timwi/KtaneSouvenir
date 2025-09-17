using System;

namespace Souvenir;

/// <summary>Used when a Souvenir module processor encounters a condition that requires a module to be abandoned.</summary>
internal sealed class AbandonModuleException(string message) : Exception(message)
{
}
