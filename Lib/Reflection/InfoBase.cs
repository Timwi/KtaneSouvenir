using System;
using System.Collections;
using System.Linq;

namespace Souvenir.Reflection
{
    abstract class InfoBase<T>
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
            if (validator != null && (validatorFailMessage = validator(value)) != null)
                throw new AbandonModuleException($"{LoggingString} with value {stringify(value)} did not pass validity check: {validatorFailMessage}.");
            return value;
        }

        public T GetFrom(object obj, Func<T, string> validator = null, bool nullAllowed = false)
        {
            var value = GetValue(obj);
            if (!nullAllowed && value == null)
                throw new AbandonModuleException($"{LoggingString} is null.");
            string validatorFailMessage;
            if (validator != null && (validatorFailMessage = validator(value)) != null)
                throw new AbandonModuleException($"{LoggingString} with value {stringify(value)} did not pass validity check: {validatorFailMessage}.");
            return value;
        }

        protected string stringify(object value)
        {
            if (value == null)
                return "<null>";
            if (value is IList list)
                return $"[{list.Cast<object>().Select(stringify).JoinString(", ")}]";
            return $"“{value}”";
        }
    }
}
