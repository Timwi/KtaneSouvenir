using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMinskMetro
{
    [SouvenirQuestion("What was the name of starting station in {0}?", OneColumn4Answers, ExampleAnswers = ["Uručča", "Kamiennaja Horka", "Park Čaluskincaŭ", "Płošča Jakuba Kołasa"])]
    Station
}

public partial class SouvenirModule
{
    [SouvenirHandler("minskMetro", "Minsk Metro", typeof(SMinskMetro), "rand06")]
    private IEnumerator<SouvenirInstruction> ProcessMinskMetro(ModuleData module)
    {
        var comp = GetComponent(module, "minskMetroScript");
        var correctAnswer = GetField<string>(comp, "initStation").Get();
        var wrongAnswers = GetField<string[]>(comp, "otherStations").Get();
        yield return WaitForSolve;
        addQuestion(module, Question.MinskMetroStation, correctAnswers: new[] { correctAnswer }, preferredWrongAnswers: wrongAnswers);
    }
}