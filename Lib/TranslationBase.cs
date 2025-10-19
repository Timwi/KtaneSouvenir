using System;
using System.Collections.Generic;

namespace Souvenir;

public abstract class TranslationBase<T> : ITranslation where T : TranslationInfo
{
    protected abstract Dictionary<Type, T> _translations { get; }
    public abstract string[] IntroTexts { get; }
    public virtual int DefaultFontIndex => 0;
    public virtual float LineSpacing => 0.525f;

    public virtual string FormatModuleName(SouvenirQuestionAttribute qAttr, bool addSolveCount, int numSolved) =>
        addSolveCount ? $"the {qAttr.Handler.ModuleName} you solved {Ordinal(numSolved)}" : qAttr.Handler.ModuleNameWithThe;
    public abstract string Ordinal(int number);

    private Dictionary<Type, T> _translationsCache = null;
    public TranslationInfo TranslateModule(Type enumType) => (_translationsCache ??= _translations).Get(enumType);
    public QuestionTranslationInfo TranslateQuestion(Enum enumValue) =>
        TranslateModule(enumValue.GetQuestionAttribute().Handler.EnumType)?.GetQuestion(enumValue);
    public DiscriminatorTranslationInfo TranslateDiscriminator(Enum enumValue) =>
        TranslateModule(enumValue.GetDiscriminatorAttribute().Handler.EnumType)?.GetDiscriminator(enumValue);
}
