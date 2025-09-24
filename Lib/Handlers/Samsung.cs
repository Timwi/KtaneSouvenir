using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSamsung
{
    [SouvenirQuestion("Where was {1} in {0}?", ThreeColumns6Answers, Arguments = ["Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord"], ArgumentGroupSize = 1, Type = AnswerType.Sprites)]
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
            yield return question(SSamsung.AppPositions, args: [appNames[i]]).Answers(new Coord(3, 3, appPositions[i]));
    }
}
