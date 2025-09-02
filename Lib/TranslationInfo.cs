using System;
using System.Collections.Generic;

namespace Souvenir;

public class TranslationInfo
{
    public bool NeedsTranslation = false;
    public string ModuleName;

    public Dictionary<Enum, QuestionTranslationInfo> Questions;
    public Dictionary<Enum, DiscriminatorTranslationInfo> Discriminators;

    public static Dictionary<string, ITranslation> AllTranslations = new()
    {
        ["de"] = new Translation_de(),
        ["ja"] = new Translation_ja(),
        ["ru"] = new Translation_ru()
    };
}
