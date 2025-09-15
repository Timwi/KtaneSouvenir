using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSynonyms
{
    [SouvenirQuestion("Which number was displayed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("synonyms", "Synonyms", typeof(SSynonyms), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSynonyms(ModuleData module)
    {
        var comp = GetComponent(module, "Synonyms");
        var numberText = GetField<TextMesh>(comp, "NumberText", isPublic: true).Get();

        yield return numberText.text == null || !int.TryParse(numberText.text, out var number) || number < 0 || number > 9
            ? throw new AbandonModuleException($"The display text (“{numberText.text ?? "<null>"}”) is not an integer 0–9.")
            : (YieldInstruction) WaitForSolve;
        numberText.gameObject.SetActive(false);
        GetField<TextMesh>(comp, "BadLabel", isPublic: true).Get().text = "INPUT";
        GetField<TextMesh>(comp, "GoodLabel", isPublic: true).Get().text = "ACCEPTED";

        addQuestion(module, Question.SynonymsNumber, correctAnswers: new[] { number.ToString() });
    }
}