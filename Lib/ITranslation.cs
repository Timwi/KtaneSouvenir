using System;

namespace Souvenir;

public interface ITranslation
{
    string[] IntroTexts { get; }
    int DefaultFontIndex { get; }
    float LineSpacing { get; }

    string FormatModuleName(Type enumType, bool addSolveCount, int numSolved);
    string Ordinal(int number);
    TranslationInfo Translate(Type enumType);
}
