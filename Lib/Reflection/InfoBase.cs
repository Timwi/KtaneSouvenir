using System;

namespace Souvenir.Reflection;

internal abstract class InfoBase<T>(object target)
{
    protected readonly object _target = target;
    protected abstract string LoggingString { get; }

    protected T validate(T value, Func<T, string> validator, bool nullAllowed, string format1 = "{0} is null.", string format2 = "{0} with value {1} did not pass validity check: {2}.")
    {
        if (!nullAllowed && value == null)
            throw new AbandonModuleException(string.Format(format1, LoggingString));
        string validatorFailMessage;
        return validator != null && (validatorFailMessage = validator(value)) != null
            ? throw new AbandonModuleException(string.Format(format2, LoggingString, value.Stringify(), validatorFailMessage))
            : value;
    }
}
