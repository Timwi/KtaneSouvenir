using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHoldUps
{
    [SouvenirQuestion("What was the name of the {1} shadow shown in {0}?", OneColumn4Answers, "Mandrake", "Silky", "Koropokguru", "Nue", "Jack Frost", "Leanan Sidhe", "Hua Po", "Orthrus", "Lamia", "Bicorn", "Kelpie", "Apsaras", "Makami", "Nekomata", "Sandman", "Naga", "Agathion", "Berith", "Mokoi", "Inugami", "High Pixie", "Yaksini", "Anzu", "Take-Minakata", "Thoth", "Isis", "Incubis", "Onmoraki", "Koppa-Tengu", "Orobas", "Rakshasa", "Pixie", "Angel", "Jack O' Lantern", "Succubus", "Andras", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Shadows
}

public partial class SouvenirModule
{
    [SouvenirHandler("KritHoldUps", "Hold Ups", typeof(SHoldUps), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessHoldUps(ModuleData module)
    {
        var comp = GetComponent(module, "HoldUpsScript");

        var stageNumber = GetField<int>(comp, "StageNr");
        var isItFiveStages = GetField<bool>(comp, "FiveDowns");

        var shadows = new List<string>();
        var holdUps = Enumerable.Range(1, 4).Select(btn => GetField<KMSelectable>(comp, $"Move{btn}Button", isPublic: true).Get()).ToArray();
        var prevInteracts = holdUps.Select(btn => btn.OnInteract).ToArray();

        foreach (var btn in Enumerable.Range(0, holdUps.Length))
        {
            holdUps[btn].OnInteract = delegate
            {
                if (shadows.Count < stageNumber.Get())
                    shadows.Add(GetField<TextMesh>(comp, "ShadowName", isPublic: true).Get().text);
                return prevInteracts[btn]();
            };
        }

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, isItFiveStages.Get() ? 5 : 3).Select(stage => makeQuestion(SHoldUps.Shadows, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { shadows[stage] })));
    }
}