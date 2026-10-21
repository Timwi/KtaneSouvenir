using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum STau
{
	[Question("What was the {1} letter of the display in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letters
}

public partial class SouvenirModule
{
	[Handler("tauModule", "Tau", typeof(STau), "Infusion")]
	[ManualQuestion("What were the letters shown?")]
	private IEnumerator<SouvenirInstruction> ProcessTau(ModuleData module)
    {
        var comp = GetComponent(module, "Tau");
        var letters = GetListField<string>(comp, "encryptedText").Get(expectedLength: 5);

        yield return WaitForSolve;

        for (var i = 0; i < letters.Count; i++)
            yield return question(STau.Letters, args: [Ordinal(i + 1)]).Answers(letters[i], preferredWrong: letters.ToArray());
    }
}