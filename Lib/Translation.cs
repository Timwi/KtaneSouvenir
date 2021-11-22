using System.Collections.Generic;

namespace Souvenir
{
    public abstract class Translation
    {
        public abstract Dictionary<Question, TranslationInfo> Translations { get; }
    }
}
