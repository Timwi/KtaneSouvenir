using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STimezone
{
    [SouvenirQuestion("What was the {1} city in {0}?", TwoColumns4Answers, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa", Arguments = ["departure", "destination"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Cities
}

public partial class SouvenirModule
{
    [SouvenirHandler("timezone", "Timezone", typeof(STimezone), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTimezone(ModuleData module)
    {
        var comp = GetComponent(module, "TimezoneScript");
        var fldFromCity = GetField<string>(comp, "from");
        var fldToCity = GetField<string>(comp, "to");
        var textFromCity = GetField<TextMesh>(comp, "TextFromCity", isPublic: true).Get();
        var textToCity = GetField<TextMesh>(comp, "TextToCity", isPublic: true).Get();

        yield return fldFromCity.Get() != textFromCity.text || fldToCity.Get() != textToCity.text
            ? throw new AbandonModuleException($"The city names don’t match up: “{fldFromCity.Get()}” vs. “{textFromCity.text}” and “{fldToCity.Get()}” vs. “{textToCity.text}”.")
            : (YieldInstruction) WaitForSolve;
        textFromCity.text = "WELL";
        textToCity.text = "DONE!";
        yield return question(STimezone.Cities, args: ["departure"]).Answers(fldFromCity.Get());
        yield return question(STimezone.Cities, args: ["destination"]).Answers(fldToCity.Get());
    }
}