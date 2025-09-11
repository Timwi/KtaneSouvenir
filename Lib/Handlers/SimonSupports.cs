using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSupports
{
    [SouvenirQuestion("What was the {1} topic in {0}?", TwoColumns4Answers, "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated", Arguments = [QandA.Ordinal], TranslateAnswers = true, ArgumentGroupSize = 1)]
    Topics
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSupports", "Simon Supports", typeof(SSimonSupports), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSupports(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSupportsScript");
        yield return WaitForSolve;

        var combo = GetField<bool[][]>(comp, "combo").Get();
        var traits = GetArrayField<int>(comp, "tra").Get(expectedLength: 8);
        var traitNames = new[] { "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated" };
        var chosenTopics = Enumerable.Range(0, 3).Select(x => traitNames[traits[x]]).ToArray();

        var qs = new List<QandA>();
        for (var i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.SimonSupportsTopics, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { chosenTopics[i] }, preferredWrongAnswers: chosenTopics));
        addQuestions(module, qs);
    }
}