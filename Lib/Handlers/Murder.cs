using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMurder
{
    [SouvenirQuestion("Which of these was {1} in {0}?", TwoColumns4Answers, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White", TranslateAnswers = true, Arguments = ["a suspect but not the murderer", "not a suspect"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Suspect,

    [SouvenirQuestion("Which of these was {1} in {0}?", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", TranslateAnswers = true, Arguments = ["a potential weapon but not the murder weapon", "not a potential weapon"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Weapon,

    [SouvenirQuestion("Where was the body found in {0}?", TwoColumns4Answers, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library", TranslateAnswers = true)]
    BodyFound
}

public partial class SouvenirModule
{
    [SouvenirHandler("murder", "Murder", typeof(SMurder), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMurder(ModuleData module)
    {
        var comp = GetComponent(module, "MurderModule");

        // Just a consistency check
        GetIntField(comp, "suspects").Get(min: 4, max: 4);
        GetIntField(comp, "weapons").Get(min: 4, max: 4);

        yield return WaitForSolve;

        var solution = GetArrayField<int>(comp, "solution").Get(expectedLength: 3);
        var skipDisplay = GetField<int[,]>(comp, "skipDisplay").Get(ar => ar.GetLength(0) != 2 || ar.GetLength(1) != 6 ? $"dimensions are {ar.GetLength(0)},{ar.GetLength(1)}; expected 2,6" : null);
        var names = GetField<string[,]>(comp, "names").Get(ar => ar.GetLength(0) != 3 || ar.GetLength(1) != 9 ? $"dimensions are {ar.GetLength(0)},{ar.GetLength(1)}; expected 3,9" : null);
        var actualSuspect = solution[0];
        var actualWeapon = solution[1];
        var actualRoom = solution[2];
        var bodyFound = GetIntField(comp, "bodyFound").Get();
        if (actualSuspect < 0 || actualSuspect >= 6 || actualWeapon < 0 || actualWeapon >= 6 || actualRoom < 0 || actualRoom >= 9 || bodyFound < 0 || bodyFound >= 9)
            throw new AbandonModuleException($"Unexpected suspect, weapon, room or bodyFound (expected 0–5/0–5/0–8/0–8, got {actualSuspect}/{actualWeapon}/{actualRoom}/{bodyFound}).");

        yield return question(SMurder.Suspect, args: ["a suspect but not the murderer"])
            .Answers(Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 0 && suspectIx != actualSuspect).Select(suspectIx => names[0, suspectIx]).ToArray());
        yield return question(SMurder.Suspect, args: ["not a suspect"])
            .Answers(Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 1).Select(suspectIx => names[0, suspectIx]).ToArray());
        yield return question(SMurder.Weapon, args: ["a potential weapon but not the murder weapon"])
            .Answers(Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 0 && weaponIx != actualWeapon).Select(weaponIx => names[1, weaponIx]).ToArray());
        yield return question(SMurder.Weapon, args: ["not a potential weapon"])
            .Answers(Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 1).Select(weaponIx => names[1, weaponIx]).ToArray());

        if (bodyFound != actualRoom)
            yield return question(SMurder.BodyFound).Answers(names[2, bodyFound]);
    }
}
