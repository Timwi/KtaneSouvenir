using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotColoredSwitches
{
    [SouvenirQuestion("What was the encrypted word in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Adjust", "Anchor", "Bowtie", "Button", "Cipher", "Corner"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotColoredSwitchesModule", "Not Colored Switches", typeof(SNotColoredSwitches), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotColoredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "NotColoredSwitchesScript");
        yield return WaitForSolve;
        var wordList = GetStaticField<string[]>(comp.GetType(), "_wordList").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        var solutionWordRaw = GetField<string>(comp, "_chosenWord").Get();
        var solutionWord = solutionWordRaw.Substring(0, 1) + solutionWordRaw.Substring(1).ToLowerInvariant();

        addQuestion(module, Question.NotColoredSwitchesWord, correctAnswers: new[] { solutionWord }, preferredWrongAnswers: wordList);
    }
}