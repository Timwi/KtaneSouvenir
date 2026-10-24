using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum STau
{
	[Question("What was the {1} letter of the display in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLetters,

    [Discriminator("the Tau whose {0} letter was {1}", Arguments = [QandA.Ordinal, "T", QandA.Ordinal, "A", QandA.Ordinal, "U"], ArgumentGroupSize = 2)]
    DLetters
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
        {
            yield return new Discriminator(STau.DLetters, $"letter{i}", letters[i], args: [Ordinal(i + 1), letters[i]]);
            yield return question(STau.QLetters, args: [Ordinal(i + 1)]).AvoidDiscriminators($"letter{i}").Answers(letters[i], preferredWrong: letters.ToArray());
        }
    }
}
