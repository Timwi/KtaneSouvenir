using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUfoSatellites
{
    [SouvenirQuestion("Which of those numbers was NOT present on {0}?", TwoColumns4Answers, ExampleAnswers = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"])]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("UfoSatellites", "UFO Satellites", typeof(SUfoSatellites), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessUfoSatellites(ModuleData module)
    {
        // allNumbers Array contains all 6 Numbers, and Souvenir will randomly pick 3 different ones
        // (it is guaranteed to have 3 different numbers. See UfoSatellites' code for why.)
        var comp = GetComponent(module, "UfoSatellitesScript");
        var allNumbers = GetField<int[]>(comp, "satelliteNumbers").Get();

        // No need to do an array with only unique answers, Souvenir does it by itself
        string[] numbersAsStrings = new string[]{ allNumbers[0].ToString(), allNumbers[1].ToString(), allNumbers[2].ToString(), allNumbers[3].ToString(), allNumbers[4].ToString(), allNumbers[5].ToString() };

        // Get a number that isn't present.
        bool searchingForNumber = true;
        string answerNumber = "0";
        while (searchingForNumber)
        {
            answerNumber = UnityEngine.Random.Range(0, 10).ToString();
            if (!numbersAsStrings.Contains(answerNumber))
            {
                searchingForNumber = false;
            }
        }


        yield return WaitForSolve;

        yield return question(SUfoSatellites.Numbers).Answers(answerNumber, preferredWrong: numbersAsStrings);
    }
}
