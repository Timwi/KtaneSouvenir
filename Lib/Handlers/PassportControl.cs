using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SPassportControl
{
    [SouvenirQuestion("What was the passport expiration year of the {1} inspected passenger in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1931", "1956", "1977", "1980", "1991", "2000", "2004", "2019", "2047"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Passenger
}

public partial class SouvenirModule
{
    [SouvenirHandler("passportControl", "Passport Control", typeof(SPassportControl), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessPassportControl(ModuleData module)
    {
        var comp = GetComponent(module, "passportControlScript");

        if (GetField<bool>(comp, "allPass").Get() || GetField<bool>(comp, "allDeny").Get())
            yield return legitimatelyNoQuestion(module, "One of the unicorn rules (all pass or all deny) was in effect.");

        var fldPassages = GetIntField(comp, "passages");
        var fldExpiration = GetArrayField<int>(comp, "expiration");
        var stamps = GetArrayField<KMSelectable>(comp, "stamps", isPublic: true).Get();
        var textToHide1 = GetArrayField<GameObject>(comp, "passport", isPublic: true).Get(validator: objs => objs.Any(go => go.GetComponent<TextMesh>() == null) ? "doesn’t have a TextMesh component" : null);
        var textToHide2 = GetField<GameObject>(comp, "ticket", isPublic: true).Get(go => go.GetComponent<TextMesh>() == null ? "doesn’t have a TextMesh component" : null);

        var expirationDates = new List<int>();

        for (var i = 0; i < stamps.Length; i++)
        {
            var oldHandler = stamps[i].OnInteract;
            stamps[i].OnInteract = delegate
            {
                // Only add the expiration date if there is no error. The error is caught later when the length of ‘expirationDates’ is checked.
                // Avoid throwing exceptions inside of the button handler
                var date = fldExpiration.Get(nullAllowed: true);
                if (date == null || date.Length != 3)
                    return oldHandler();

                var year = date[2];
                var passages = fldPassages.Get();
                var ret = oldHandler();
                if (fldPassages.Get() == passages) // player got strike, ignoring retrieved info
                    return ret;

                expirationDates.Add(year);
                return ret;
            };
        }

        yield return WaitForSolve;

        if (expirationDates.Count != 3)
            throw new AbandonModuleException($"The number of retrieved sets of information was {expirationDates.Count} (expected 3).");

        for (var i = 0; i < textToHide1.Length; i++)
            textToHide1[i].GetComponent<TextMesh>().text = "";
        textToHide2.GetComponent<TextMesh>().text = "";

        var altDates = new List<string[]>();

        for (var i = 0; i < expirationDates.Count; i++)
        {
            altDates.Add(new string[6]);
            var startVal = expirationDates[i] - Rnd.Range(0, 6);
            for (var j = 0; j < altDates[i].Length; j++)
                altDates[i][j] = (startVal + j).ToString();
        }

        for (var i = 0; i < 3; i++)
            yield return question(SPassportControl.Passenger, args: [Ordinal(i + 1)]).Answers(expirationDates[i].ToString(), preferredWrong: altDates[i]);
    }
}
