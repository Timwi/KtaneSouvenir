using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STopsyTurvy
{
    [SouvenirQuestion("What was the word initially shown in {0}?", ThreeColumns6Answers, "Topsy", "Robot", "Cloud", "Round", "Quilt", "Found", "Plaid", "Curve", "Water", "Ovals", "Verse", "Sandy", "Frown", "Windy", "Curse", "Ghost")]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("topsyTurvy", "Topsy Turvy", typeof(STopsyTurvy), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessTopsyTurvy(ModuleData module)
    {
        var comp = GetComponent(module, "topsyTurvy");
        yield return WaitForSolve;

        yield return question(STopsyTurvy.Word).Answers(Question.TopsyTurvyWord.GetAnswers()[GetField<int>(comp, "displayIndex").Get()]);
    }
}