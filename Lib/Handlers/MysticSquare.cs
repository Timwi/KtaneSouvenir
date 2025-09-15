using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMysticSquare
{
    [SouvenirQuestion("Where was the skull in {0}?", TwoColumns4Answers, "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right", TranslateAnswers = true)]
    Skull
}

public partial class SouvenirModule
{
    [SouvenirHandler("MysticSquareModule", "Mystic Square", typeof(SMysticSquare), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessMysticSquare(ModuleData module)
    {
        var comp = GetComponent(module, "MysticSquareModule");
        var skull = GetField<Transform>(comp, "Skull", true).Get();

        while (!skull.gameObject.activeSelf)
            yield return null;

        yield return WaitForSolve;

        var skullpos = GetIntField(comp, "_skullPos").Get(min: 0, max: 8);
        var spacepos = Array.IndexOf(GetArrayField<int>(comp, "_field").Get(), 0);

        // If the skull is in the empty space, shrink and then disappear it.
        if (skullpos == spacepos)
        {
            // Make sure that the last sliding animation finishes
            yield return new WaitForSeconds(0.5f);

            const float duration = 1.5f;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                skull.localScale = Vector3.Lerp(new Vector3(0.004f, 0.004f, 0.004f), Vector3.zero, elapsed / duration);
                yield return null;
                elapsed += Time.deltaTime;
            }
        }

        skull.gameObject.SetActive(false);
        var answers = new[] { "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right" };
        yield return question(SMysticSquare.Skull).Answers(answers[skullpos]);
    }
}