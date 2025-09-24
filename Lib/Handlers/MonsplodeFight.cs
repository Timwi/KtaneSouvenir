using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMonsplodeFight
{
    [SouvenirQuestion("Which creature was displayed in {0}?", TwoColumns4Answers, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie")]
    Creature,

    [SouvenirQuestion("Which one of these moves {1} selectable in {0}?", TwoColumns4Answers, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep", Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Move
}

public partial class SouvenirModule
{
    [SouvenirHandler("monsplodeFight", "Monsplode, Fight!", typeof(SMonsplodeFight), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMonsplodeFight(ModuleData module)
    {
        var comp = GetComponent(module, "MonsplodeFightModule");
        var fldCreatureID = GetIntField(comp, "crID");
        var fldMoveIDs = GetArrayField<int>(comp, "moveIDs");
        var fldRevive = GetField<bool>(comp, "revive");

        yield return WaitForActivate;

        var creatureData = GetField<object>(comp, "CD", isPublic: true).Get();
        var movesData = GetField<object>(comp, "MD", isPublic: true).Get();
        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get(expectedLength: 4);
        var creatureNames = GetArrayField<string>(creatureData, "names", isPublic: true).Get();
        var moveNames = GetArrayField<string>(movesData, "names", isPublic: true).Get();

        string displayedCreature = null;
        string[] displayedMoves = null;
        var finished = false;

        var origInteracts = buttons.Select(btn => btn.OnInteract).ToArray();
        foreach (var i in Enumerable.Range(0, buttons.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            buttons[i].OnInteract = delegate
            {
                // Before processing the button push, get the creature and moves
                string curCreatureName = null;
                string[] curMoveNames = null;

                var creatureID = fldCreatureID.Get();
                if (creatureID == -1)
                {
                    // Missingno: do nothing
                }
                else if (creatureID < 0 || creatureID >= creatureNames.Length || string.IsNullOrEmpty(creatureNames[creatureID]))
                    Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Unexpected creature ID: {creatureID}; creature names are: [{creatureNames.Select(cn => cn == null ? "null" : '"' + cn + '"').JoinString(", ")}]");
                else
                {
                    // Make sure not to throw exceptions inside of the module’s button handler!
                    var moveIDs = fldMoveIDs.Get(nullAllowed: true);
                    if (moveIDs == null || moveIDs.Length != 4 || moveIDs.Any(mid => mid >= moveNames.Length || string.IsNullOrEmpty(moveNames[mid])))
                        Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Unexpected move IDs: {(moveIDs == null ? null : "[" + moveIDs.JoinString(", ") + "]")}; moves names are: [{moveNames.Select(mn => mn == null ? "null" : '"' + mn + '"').JoinString(", ")}]");
                    else
                    {
                        curCreatureName = creatureNames[creatureID];
                        curMoveNames = moveIDs.Select(mid => moveNames[mid].Replace("\r", "").Replace("\n", " ")).ToArray();
                    }
                }

                var ret = origInteracts[i]();

                if (creatureID == -1)
                {
                    legitimatelyNoQuestion(module, "The creature displayed was Missingno.");
                    displayedCreature = null;
                    displayedMoves = null;
                    finished = true;
                }
                else if (curCreatureName == null || curMoveNames == null)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Abandoning due to error above.");
                    // Set these to null to signal that something went wrong and we need to abort
                    displayedCreature = null;
                    displayedMoves = null;
                    finished = true;
                }
                else
                {
                    // If ‘revive’ is ‘false’, there is not going to be another stage.
                    if (!fldRevive.Get())
                        finished = true;

                    if (curCreatureName != null && curMoveNames != null)
                    {
                        displayedCreature = curCreatureName;
                        displayedMoves = curMoveNames;
                    }
                }
                return ret;
            };
        }

        while (!finished)
            yield return null;
        yield return WaitForSolve;

        for (var i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = origInteracts[i];

        // If either of these is the case, an error message will already have been output above (within the button handler)
        if (displayedCreature == null || displayedMoves == null)
            yield break;

        yield return question(SMonsplodeFight.Creature).Answers(displayedCreature);
        yield return question(SMonsplodeFight.Move, args: ["was"]).Answers(displayedMoves);
        yield return question(SMonsplodeFight.Move, args: ["was not"]).Answers(SMonsplodeFight.Move.GetAnswers().Except(displayedMoves).ToArray());
    }
}
