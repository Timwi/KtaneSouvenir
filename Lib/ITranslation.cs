namespace Souvenir
{
    public interface ITranslation
    {
        string[] IntroTexts { get; }
        int DefaultFontIndex { get; }
        float LineSpacing { get; }

        public abstract string FormatModuleName(Question question, bool addSolveCount, int numSolved);
        public abstract string Ordinal(int number);
        public TranslationInfo Translate(Question question);
    }
}
