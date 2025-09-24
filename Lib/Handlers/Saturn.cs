using System.Collections;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSaturn
{
    [SouvenirQuestion("Where was the goal in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), new object[] { new string[] { "0-9", " " } }, typeof(AnswerGenerator.Integers), new object[] { 0, 63 })]
    Goal
}

public partial class SouvenirModule
{
    [SouvenirHandler("saturn", "Saturn", typeof(SSaturn), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessSaturn(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "SaturnScript");

        var hideButton = GetField<KMSelectable>(comp, "HideButton", true).Get();
        if (!TwitchPlaysActive && hideButton.OnInteract is not null)
            StartCoroutine(GetMethod<IEnumerator>(comp, "HidePlanet", 0).Invoke());
        hideButton.OnInteract = null;

        var index = GetIntField(comp, "EndIndex").Get(min: 0, max: 64 * 5);
        var outer = GetField<bool>(comp, "EndOuter").Get();

        yield return question(SSaturn.Goal).Answers($"{(outer ? 9 : 4) - (index / 64)} {index % 64}");
    }
}