using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum STicTacToe
{
    [SouvenirQuestion("What was on the {1} button at the start of {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X", Type = AnswerType.TicTacToeFont, Arguments = ["top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    InitialState,

    [SouvenirDiscriminator("the Tic Tac Toe where the {0} button was {1}", Arguments = ["top-left", "1", "top-middle", "2", "top-right", "3", "middle-left", "4", "middle-center", "5", "middle-right", "6", "bottom-left", "7", "bottom-middle", "8", "bottom-right", "9", "bottom-right", "O", "bottom-right", "X"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("TicTacToeModule", "Tic Tac Toe", typeof(STicTacToe), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTicTacToe(ModuleData module)
    {
        var comp = GetComponent(module, "TicTacToeModule");
        var fldIsInitialized = GetField<bool>(comp, "_isInitialized");

        while (!fldIsInitialized.Get())
            yield return null;

        var keypadButtons = GetArrayField<KMSelectable>(comp, "KeypadButtons", isPublic: true).Get(expectedLength: 9);
        var keypadPhysical = GetArrayField<KMSelectable>(comp, "_keypadButtonsPhysical").Get(expectedLength: 9);

        // Take a copy of the placedX array because it changes
        var placedX = GetArrayField<bool?>(comp, "_placedX").Get(expectedLength: 9, nullContentAllowed: true).ToArray();

        yield return WaitForSolve;

        var buttonNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        for (var ix = 0; ix < 9; ix++)
        {
            var buttonName = buttonNames[Array.IndexOf(keypadPhysical, keypadButtons[ix])];
            var value = placedX[ix] == null ? (ix + 1).ToString() : placedX[ix].Value ? "X" : "O";
            yield return new Discriminator(STicTacToe.Discriminator, $"position{ix}", value, args: [buttonName, value]);
            yield return question(STicTacToe.InitialState, args: [buttonName]).AvoidDiscriminators($"position{ix}").Answers(value);
        }
    }
}
