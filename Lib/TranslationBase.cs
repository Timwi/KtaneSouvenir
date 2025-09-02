using System;
using System.Collections.Generic;

namespace Souvenir;

public abstract class TranslationBase<T> : ITranslation where T : TranslationInfo
{
    protected abstract Dictionary<Type, T> _translations { get; }
    public abstract string[] IntroTexts { get; }
    public virtual int DefaultFontIndex => 0;
    public virtual float LineSpacing => 0.525f;

    public virtual string FormatModuleName(Type enumType, bool addSolveCount, int numSolved) =>
        addSolveCount ? $"the {enumType.GetHandlerAttribute().ModuleName} you solved {Ordinal(numSolved)}" : enumType.GetHandlerAttribute().ModuleNameWithThe;
    public abstract string Ordinal(int number);

    private Dictionary<Type, T> _translationsCache = null;
    public TranslationInfo Translate(Type enumType) => (_translationsCache ??= _translations).Get(enumType);
}
