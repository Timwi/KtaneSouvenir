using System;
using System.Collections.Generic;

namespace Souvenir;

public abstract class TranslationInfo
{
    public bool NeedsTranslation = false;
    public string ModuleName;

    public abstract QuestionTranslationInfo GetQuestion(Enum enumValue);
    public abstract DiscriminatorTranslationInfo GetDiscriminator(Enum enumValue);

    public static Dictionary<string, ITranslation> AllTranslations = new()
    {
        ["de"] = new Translation_de(),
        ["ja"] = new Translation_ja(),
        ["ru"] = new Translation_ru()
    };
}

public class TranslationInfo<TQ> : TranslationInfo where TQ : QuestionTranslationInfo
{
    public Dictionary<Enum, TQ> Questions;
    public Dictionary<Enum, DiscriminatorTranslationInfo> Discriminators;

    public override QuestionTranslationInfo GetQuestion(Enum enumValue) => Questions.Get(enumValue);
    public override DiscriminatorTranslationInfo GetDiscriminator(Enum enumValue) => Discriminators.Get(enumValue);
}
