using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPoetry
{
    [SouvenirQuestion("What was the {1} correct answer you pressed in {0}?", TwoColumns4Answers, "clarity", "flow", "fatigue", "hollow", "energy", "sunshine", "ocean", "reflection", "identity", "black", "crowd", "heart", "weather", "words", "past", "solitary", "relax", "dance", "weightless", "morality", "gaze", "failure", "bunny", "lovely", "romance", "future", "focus", "search", "cookies", "compassion", "creation", "patience", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Answers
}

public partial class SouvenirModule
{
    [SouvenirHandler("poetry", "Poetry", typeof(SPoetry), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPoetry(ModuleData module)
    {
        var comp = GetComponent(module, "PoetryModule");
        var fldStage = GetIntField(comp, "currentStage");
        var fldStageCount = GetIntField(comp, "stageCount", isPublic: true);

        var answers = new List<string>();
        var selectables = GetArrayField<KMSelectable>(comp, "wordSelectables", isPublic: true).Get(expectedLength: 6);
        var wordTextMeshes = GetArrayField<TextMesh>(comp, "words", isPublic: true).Get(expectedLength: 6);

        for (var i = 0; i < 6; i++)
        {
            var j = i;
            var oldHandler = selectables[i].OnInteract;
            selectables[i].OnInteract = delegate
            {
                var prevStage = fldStage.Get();
                var word = wordTextMeshes[j].text;
                var ret = oldHandler();

                if (fldStage.Get() > prevStage)
                    answers.Add(word);

                return ret;
            };
        }

        yield return WaitForSolve;

        if (answers.Count != fldStageCount.Get())
            throw new AbandonModuleException($"The number of answers captured is not equal to the number of stages played ({fldStageCount.Get()}). Answers were: [{answers.JoinString(", ")}]");

        addQuestions(module, answers.Select((ans, st) => makeQuestion(SPoetry.Answers, module, formatArgs: new[] { Ordinal(st + 1) }, correctAnswers: new[] { ans }, preferredWrongAnswers: answers.ToArray())));
    }
}