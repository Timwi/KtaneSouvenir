using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBlockbusters
{
    [SouvenirQuestion("What was the last letter pressed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    LastLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("blockbusters", "Blockbusters", typeof(SBlockbusters), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessBlockbusters(ModuleData module)
    {
        var comp = GetComponent(module, "blockbustersScript");
        var legalLetters = GetListField<string>(comp, "legalLetters", isPublic: true).Get();
        var tiles = GetField<Array>(comp, "tiles", isPublic: true).Get(arr => arr.Cast<object>().Any(v => v == null) ? "contains null" : null);
        var selectables = new KMSelectable[tiles.Length];
        var prevInteracts = new KMSelectable.OnInteractHandler[tiles.Length];
        string lastPress = null;

        for (var i = 0; i < tiles.Length; i++)
        {
            var selectable = selectables[i] = GetField<KMSelectable>(tiles.GetValue(i), "selectable", isPublic: true).Get();
            var prevInteract = prevInteracts[i] = selectable.OnInteract;
            var letter = GetField<TextMesh>(tiles.GetValue(i), "containedLetter", isPublic: true).Get();
            selectable.OnInteract = delegate
            {
                lastPress = letter.text;
                return prevInteract();
            };
        }

        yield return WaitForSolve;

        for (var i = 0; i < tiles.Length; i++)
            selectables[i].OnInteract = prevInteracts[i];

        if (lastPress == null)
            throw new AbandonModuleException("No pressed letter was retrieved.");

        yield return question(SBlockbusters.LastLetter).Answers(lastPress, preferredWrong: legalLetters.ToArray());
    }
}