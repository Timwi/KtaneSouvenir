using System;

namespace Souvenir
{
    /// <summary>Used when a Souvenir module processor encounters a condition that requires a module to be abandoned.</summary>
    sealed class AbandonModuleException : Exception
    {
        public AbandonModuleException(string messageFormat, params object[] formatArgs) : base(string.Format(messageFormat, formatArgs)) { }
    }
}
