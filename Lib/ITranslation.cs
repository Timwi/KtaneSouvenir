using System;

namespace Souvenir;

public interface ITranslation
{
    string[] IntroTexts { get; }
    int DefaultFontIndex { get; }
    float LineSpacing { get; }

    string FormatModuleName(SouvenirQuestionAttribute qAttr, bool addSolveCount, int numSolved);
    string Ordinal(int number);
    TranslationInfo TranslateModule(Type enumType);
    QuestionTranslationInfo TranslateQuestion(Enum enumValue);
    DiscriminatorTranslationInfo TranslateDiscriminator(Enum enumValue);
}
