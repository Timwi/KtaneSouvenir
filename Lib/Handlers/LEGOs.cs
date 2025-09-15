using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLEGOs
{
    [SouvenirQuestion("What were the dimensions of the {1} piece in {0}?", ThreeColumns6Answers, "2×2", "3×1", "3×2", "4×1", "4×2", TranslateArguments = [true], Arguments = ["red", "green", "blue", "cyan", "magenta", "yellow"], ArgumentGroupSize = 1)]
    PieceDimensions
}

public partial class SouvenirModule
{
    [SouvenirHandler("LEGOModule", "LEGOs", typeof(SLEGOs), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessLEGOs(ModuleData module)
    {
        var comp = GetComponent(module, "LEGOModule");

        var solutionStruct = GetField<object>(comp, "SolutionStructure").Get();
        var pieces = GetField<IList>(solutionStruct, "Pieces", isPublic: true).Get(ar => ar.Count != 6 ? "expected length 6" : null);

        yield return WaitForSolve;

        // Block the left/right buttons so the player can’t see the instruction pages anymore
        var leftButton = GetField<KMSelectable>(comp, "LeftButton", isPublic: true).Get();
        var rightButton = GetField<KMSelectable>(comp, "RightButton", isPublic: true).Get();

        leftButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            leftButton.AddInteractionPunch(0.5f);
            return false;
        };
        rightButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.Module.transform);
            rightButton.AddInteractionPunch(0.5f);
            return false;
        };

        // Erase the solution so the player can’t see brick sizes on it either
        var submission = GetArrayField<int>(comp, "Submission").Get();
        for (var i = 0; i < submission.Length; i++)
            submission[i] = 0;
        GetMethod(comp, "UpdateDisplays", numParameters: 0).Invoke();

        // Obtain the brick sizes and colors
        var fldBrickColors = GetIntField(pieces[0], "BrickColor", isPublic: true);
        var fldBrickDimensions = GetArrayField<int>(pieces[0], "Dimensions", isPublic: true);
        var brickColors = Enumerable.Range(0, 6).Select(i => fldBrickColors.GetFrom(pieces[i])).ToList();
        var brickDimensions = Enumerable.Range(0, 6).Select(i => fldBrickDimensions.GetFrom(pieces[i])).ToList();

        var colorNames = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" };
        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(SLEGOs.PieceDimensions, module, formatArgs: new[] { colorNames[brickColors[i]] }, correctAnswers: new[] { brickDimensions[i][0] + "×" + brickDimensions[i][1] })));
    }
}