using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSugarSkulls
{
    [Question("Which skull {1} present in {0}?", TwoColumns4Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z", AnswerType = InfoType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Availability
}

public partial class SouvenirModule
{
    [Handler("sugarSkulls", "Sugar Skulls", typeof(SSugarSkulls), "BigCrunch22")]
    [ManualQuestion("What skulls were shown?")]
    private IEnumerator<SouvenirInstruction> ProcessSugarSkulls(ModuleData module)
    {
        var comp = GetComponent(module, "sugarSkulls");
        yield return WaitForSolve;

        var skulls = new List<string>();
        var textInfo = GetArrayField<TextMesh>(comp, "texts", isPublic: true).Get();
        for (var x = 0; x < textInfo.Length; x++)
            skulls.Add(textInfo[x].text);

        yield return question(SSugarSkulls.Availability, args: ["was"]).Answers(skulls.ToArray());
        yield return question(SSugarSkulls.Availability, args: ["was not"]).Answers(SSugarSkulls.Availability.GetAnswers().Except(skulls).ToArray());
    }
}
