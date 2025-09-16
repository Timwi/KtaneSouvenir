using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSymbolicCoordinates
{
    [SouvenirQuestion("What was the {1} symbol in the {2} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SymbolicCoordinatesSprites", TranslateArguments = [true, false], Arguments = ["left", QandA.Ordinal, "middle", QandA.Ordinal, "right", QandA.Ordinal], ArgumentGroupSize = 2)]
    ymbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("symbolicCoordinates", "Symbolic Coordinates", typeof(SSymbolicCoordinates), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessSymbolicCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "symbolicCoordinatesScript");
        var letter1 = GetField<string>(comp, "letter1").Get();
        var letter2 = GetField<string>(comp, "letter2").Get();
        var letter3 = GetField<string>(comp, "letter3").Get();

        var stageLetters = new[] { letter1.Split(' '), letter2.Split(' '), letter3.Split(' ') };
        if (stageLetters.Any(x => x.Length != 3) || stageLetters.SelectMany(x => x).Any(y => !"ACELP".Contains(y)))
            throw new AbandonModuleException($"One of the stages has fewer than 3 symbols or symbols are of unexpected value (expected symbols “ACELP”, got “{stageLetters.Select(x => $"“{x.JoinString()}”").JoinString(", ")}”).");

        yield return WaitForSolve;
        GetField<TextMesh>(comp, "lettersText", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "digitsText", isPublic: true).Get().text = "";

        foreach (var btnFieldName in new[] { "lettersUp", "lettersDown", "digitsUp", "digitsDown" })
        {
            var btn = GetField<KMSelectable>(comp, btnFieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };
        }

        var position = new[] { "left", "middle", "right" };
        for (var stage = 0; stage < stageLetters.Length; stage++)
            for (var pos = 0; pos < stageLetters[stage].Length; pos++)
                yield return question(SSymbolicCoordinates.ymbols, args: [position[pos], Ordinal(stage + 1)])
                    .Answers(SymbolicCoordinatesSprites["ACELP".IndexOf(stageLetters[stage][pos], StringComparison.Ordinal)], preferredWrong: SymbolicCoordinatesSprites);
    }
}
