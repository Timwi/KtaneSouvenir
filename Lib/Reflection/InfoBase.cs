using System;

namespace Souvenir.Reflection
{
    internal abstract class InfoBase<T>
    {
        protected readonly object _target;
        protected abstract T GetValue();
        protected abstract T GetValue(object from);
        protected abstract string LoggingString { get; }

        public InfoBase(object target)
        {
            _target = target;
        }

        public T Get(Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = GetValue();
            if (!nullAllowed && value == null)
                throw new AbandonModuleException($"{LoggingString} is null.");
            string validatorFailMessage;
            return validator != null && (validatorFailMessage = validator(value)) != null
                ? throw new AbandonModuleException($"{LoggingString} with value {value.Stringify()} did not pass validity check: {validatorFailMessage}.")
                : value;
        }

        public T GetFrom(object obj, Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = GetValue(obj);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException($"{LoggingString} is null.");
            string validatorFailMessage;
            return validator != null && (validatorFailMessage = validator(value)) != null
                ? throw new AbandonModuleException($"{LoggingString} with value {value.Stringify()} did not pass validity check: {validatorFailMessage}.")
                : value;
        }
    }
}
