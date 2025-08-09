namespace Souvenir;

public interface ITranslation
{
    string[] IntroTexts { get; }
    int DefaultFontIndex { get; }
    float LineSpacing { get; }

    string FormatModuleName(Question question, bool addSolveCount, int numSolved);
    string Ordinal(int number);
    TranslationInfo Translate(Question question);
}
