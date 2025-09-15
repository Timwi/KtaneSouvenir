using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGirlfriend
{
    [SouvenirQuestion("What was the language sung in {0}?", TwoColumns4Answers, "English", "French", "German", "Italian", "Japanese", "Mandarin", "Portuguese", "Spanish")]
    Language
}

public partial class SouvenirModule
{
    [SouvenirHandler("Girlfriend", "Girlfriend", typeof(SGirlfriend), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessGirlfriend(ModuleData module)
    {
        var comp = GetComponent(module, "Girlfriend");
        yield return WaitForSolve;

        var languageArr = GetArrayField<string>(comp, "languages").Get();
        var answerIndex = GetIntField(comp, "answerIndex").Get(min: 0, max: languageArr.Length - 1);
        yield return question(SGirlfriend.Language).Answers(languageArr[answerIndex]);
    }
}