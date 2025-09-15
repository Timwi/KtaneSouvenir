using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSugarSkulls
{
    [SouvenirQuestion("What skull was shown on the {1} square in {0}?", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z", TranslateArguments = [true], Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, Arguments = ["top", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
    Skull,

    [SouvenirQuestion("Which skull {1} present in {0}?", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z", Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Availability
}

public partial class SouvenirModule
{
    [SouvenirHandler("sugarSkulls", "Sugar Skulls", typeof(SSugarSkulls), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessSugarSkulls(ModuleData module)
    {
        var comp = GetComponent(module, "sugarSkulls");
        yield return WaitForSolve;

        var skulls = new List<string>();
        var textInfo = GetArrayField<TextMesh>(comp, "texts", isPublic: true).Get();
        for (var x = 0; x < textInfo.Length; x++)
            skulls.Add(textInfo[x].text);

        yield return question(SSugarSkulls.Skull, args: ["top"]).Answers(skulls[0]);
        yield return question(SSugarSkulls.Skull, args: ["bottom-left"]).Answers(skulls[1]);
        yield return question(SSugarSkulls.Skull, args: ["bottom-right"]).Answers(skulls[2]);
        yield return question(SSugarSkulls.Availability, args: ["was"]).Answers(skulls.ToArray());
        yield return question(SSugarSkulls.Availability, args: ["was not"]).Answers(Question.SugarSkullsAvailability.GetAnswers().Except(skulls).ToArray());
    }
}