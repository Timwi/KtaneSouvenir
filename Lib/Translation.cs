using System.Collections.Generic;

namespace Souvenir
{
    public abstract class Translation
    {
        public abstract Dictionary<Question, TranslationInfo> Translations { get; }
        public abstract string[] IntroTexts { get; }
        public virtual int DefaultFontIndex => 0;
        public virtual float LineSpacing => 0.525f;

        public abstract string FormatModuleName(string moduleNameWithoutThe, string moduleNameWithThe, bool addSolveCount, int numSolved);
        public abstract string Ordinal(int number);

        public static Dictionary<string, Translation> AllTranslations = new()
        {
            ["de"] = new Translation_de(),
            ["eo"] = new Translation_eo(),
            ["es"] = new Translation_es(),
            ["ja"] = new Translation_ja()
        };
    }
}
