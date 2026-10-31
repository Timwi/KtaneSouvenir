using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNotGreenArrows
{
	[Question("What was the {1} letter of the display in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLetters,

    [Discriminator("the Not Green Arrows whose {0} letter was {1}", Arguments = [QandA.Ordinal, "G", QandA.Ordinal, "R", QandA.Ordinal, "N"], ArgumentGroupSize = 2)]
    DLetters
}

public partial class SouvenirModule
{
	[Handler("notGreenArrowsModule", "Not Green Arrows", typeof(SNotGreenArrows), "Infusion")]
	[ManualQuestion("What were the letters shown?")]
	private IEnumerator<SouvenirInstruction> ProcessNotGreenArrows(ModuleData module)
    {
        var comp = GetComponent(module, "NotGreenArrowsScript");

        yield return WaitForSolve;

        var letters = GetField<string>(comp, "displayedLetters").Get();

        for (var i = 0; i < 3; i++)
        {
            yield return new Discriminator(SNotGreenArrows.DLetters, $"letter{i}", letters[i], args: [Ordinal(i + 1), letters[i].ToString()]);
            yield return question(SNotGreenArrows.QLetters, args: [Ordinal(i + 1)]).AvoidDiscriminators($"letter{i}").Answers(letters[i].ToString(), preferredWrong: letters.Select(x => x.ToString()).ToArray());
        }
    }
}
