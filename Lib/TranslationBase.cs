using System.Collections.Generic;
using System.Linq;

namespace Souvenir
{
    public abstract class TranslationBase<T> : Translation where T : TranslationInfo
    {
        protected abstract Dictionary<Question, T> _translations { get; }
        public abstract string[] IntroTexts { get; }
        public virtual int DefaultFontIndex => 0;
        public virtual float LineSpacing => 0.525f;

        public abstract string FormatModuleName(Question question, bool addSolveCount, int numSolved);
        public abstract string Ordinal(int number);

        private Dictionary<Question, T> _translationsCache = null;
        public Dictionary<Question, TranslationInfo> Translations => (_translationsCache ??= _translations).ToDictionary(k => k.Key, k => (TranslationInfo)k.Value);

        public static Dictionary<string, Translation> AllTranslations = new()
        {
            ["de"] = new Translation_de(),
            ["ja"] = new Translation_ja(),
            ["ru"] = new Translation_ru()
        };
    }
}
