using System.Collections.Generic;

namespace Souvenir
{
    public abstract class Translation
    {
        protected abstract Dictionary<Question, TranslationInfo> _translations { get; }
        public abstract string[] IntroTexts { get; }
        public virtual int DefaultFontIndex => 0;
        public virtual float LineSpacing => 0.525f;

        public abstract string FormatModuleName(string moduleNameWithoutThe, string moduleNameWithThe, bool addSolveCount, int numSolved);
        public abstract string Ordinal(int number);

        private Dictionary<Question, TranslationInfo> _translationsCache = null;
        public Dictionary<Question, TranslationInfo> Translations => _translationsCache ??= _translations;

        public static Dictionary<string, Translation> AllTranslations = new()
        {
            ["de"] = new Translation_de(),
            ["ja"] = new Translation_ja()
        };
    }
}
