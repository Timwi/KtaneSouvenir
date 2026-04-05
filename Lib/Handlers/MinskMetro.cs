using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMinskMetro
{
    [Question("What was the name of starting station in {0}?", OneColumn4Answers, ExampleAnswers = ["Uručča", "Kamiennaja Horka", "Park Čaluskincaŭ", "Płošča Jakuba Kołasa"])]
    Station
}

public partial class SouvenirModule
{
    [Handler("minskMetro", "Minsk Metro", typeof(SMinskMetro), "rand06")]
    [ManualQuestion("What was the starting station?")]
    private IEnumerator<SouvenirInstruction> ProcessMinskMetro(ModuleData module)
    {
        var comp = GetComponent(module, "minskMetroScript");
        var correctAnswer = GetField<string>(comp, "initStation").Get();
        var wrongAnswers = GetField<string[]>(comp, "otherStations").Get();
        yield return WaitForSolve;
        yield return question(SMinskMetro.Station).Answers(correctAnswer, preferredWrong: wrongAnswers);
    }
}