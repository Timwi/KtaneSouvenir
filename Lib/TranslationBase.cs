using System.Collections.Generic;

namespace Souvenir;

public abstract class TranslationBase<T> : ITranslation where T : TranslationInfo
{
    protected abstract Dictionary<Question, T> _translations { get; }
    public abstract string[] IntroTexts { get; }
    public virtual int DefaultFontIndex => 0;
    public virtual float LineSpacing => 0.525f;

    public virtual string FormatModuleName(Question question, bool addSolveCount, int numSolved) =>
        addSolveCount ? $"the {question.GetAttribute().ModuleName} you solved {Ordinal(numSolved)}" : question.GetAttribute().ModuleNameWithThe;
    public abstract string Ordinal(int number);

    private Dictionary<Question, T> _translationsCache = null;
    public TranslationInfo Translate(Question question) => (_translationsCache ??= _translations).Get(question);
}
