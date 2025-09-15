using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlcoholicRampage
{
    [SouvenirQuestion("Who was the {1} mercenary displayed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AlcoholicRampageSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Mercenaries
}

public partial class SouvenirModule
{
    [SouvenirHandler("alcoholicRampageModule", "Alcoholic Rampage", typeof(SAlcoholicRampage), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessAlcoholicRampage(ModuleData module)
    {
        var comp = GetComponent(module, "AlcoholicRampageScript");
        var fldStage = GetIntField(comp, "stage");
        var fldChosenMerc = GetIntField(comp, "chosenMerc");
        var mercs = new int[3];

        while (fldStage.Get() != 3)
        {
            mercs[fldStage.Get()] = fldChosenMerc.Get();
            yield return null;
        }

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var s = 0; s < 3; s++)
            qs.Add(makeQuestion(Question.AlcoholicRampageMercenaries, module, formatArgs: new[] { Ordinal(s + 1) }, correctAnswers: new[] { AlcoholicRampageSprites[mercs[s]] }));
        addQuestions(module, qs);
    }
}
