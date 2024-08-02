using System.Collections.Generic;

namespace Souvenir
{
    public interface Translation
    {
        string[] IntroTexts { get; }
        int DefaultFontIndex { get; }
        float LineSpacing { get; }

        public abstract string FormatModuleName(Question question, bool addSolveCount, int numSolved);
        public abstract string Ordinal(int number);

        public Dictionary<Question, TranslationInfo> Translations { get; }
    }
}
