using System.Collections.Generic;

namespace Souvenir
{
    public class TranslationInfo
    {
        public string QuestionText;
        public string ModuleName;
        public Dictionary<string, string> Answers;
        public Dictionary<string, string> FormatArgs;
        public Dictionary<string, string> TranslatableStrings;
        public bool NeedsTranslation = false;

        public static Dictionary<string, ITranslation> AllTranslations = new()
        {
            ["de"] = new Translation_de(),
            ["ja"] = new Translation_ja(),
            ["ru"] = new Translation_ru()
        };
    }
}