using System.Collections.Generic;

namespace Souvenir
{
    public abstract class Translation
    {
        public abstract Dictionary<Question, TranslationInfo> Translations { get; }
        public virtual bool AllowBreakingWords => false;
        public virtual int DefaultFontIndex => 0;
        public virtual float LineSpacing => 0.525f;

        public abstract string FormatModuleName(string moduleName, bool addSolveCount, int numSolved, bool addThe);
        public abstract string Ordinal(int number);

        public static Dictionary<string, Translation> AllTranslations = new Dictionary<string, Translation>
        {
            ["ja"] = new Translation_ja(),
            ["eo"] = new Translation_eo()
        };
    }
}
