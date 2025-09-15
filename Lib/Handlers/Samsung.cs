using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSamsung
{
    [SouvenirQuestion("Where was {1} in {0}?", ThreeColumns6Answers, "TL", "TM", "TR", "ML", "MM", "MR", "BL", "BM", "BR", TranslateAnswers = true, Arguments = ["Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord"], ArgumentGroupSize = 1)]
    AppPositions
}

public partial class SouvenirModule
{
    [SouvenirHandler("theSamsung", "Samsung", typeof(SSamsung), "TasThiluna", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessSamsung(ModuleData module)
    {
        var comp = GetComponent(module, "theSamsung");
        yield return WaitForSolve;

        var appPositions = GetListField<int>(comp, "positionNumbers").Get();
        var appNames = new[] { "Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord" };
        for (var i = 0; i < 8; i++)
            yield return question(SSamsung.AppPositions, args: [appNames[i]]).Answers(SSamsung.AppPositions.GetAnswers()[appPositions[i]]);
    }
}