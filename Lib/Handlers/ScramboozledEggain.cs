using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScramboozledEggain
{
    [SouvenirQuestion("What was the {1} encrypted word in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Basted", "Boiled", "Boxing", "Carton", "Dumpty", "French"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("ScramboozledEggainModule", "Scramboozled Eggain", typeof(SScramboozledEggain), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessScramboozledEggain(ModuleData module)
    {
        var comp = GetComponent(module, "ScramboozledEggainScript");
        yield return WaitForSolve;

        var wordList = GetStaticField<string[]>(comp.GetType(), "_wordList").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        var selectedWords = GetArrayField<string>(comp, "_selectedWords").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        for (var i = 0; i < 4; i++)
            yield return question(SScramboozledEggain.Word, args: [Ordinal(i + 1)]).Answers(selectedWords[i], preferredWrong: wordList);
    }
}