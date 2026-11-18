using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SBlueButton
{
    [Question("What was N in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(4, 9)]
    N,

    [Question("What was Q in {0}?", ThreeColumns6Answers, "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", TranslateAnswers = true)]
    Q,

    [Question("What was the filling of the {1} suit (after sorting) in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AnswerType = InfoType.Sprites, SpriteFieldName = "BlueButtonSprites")]
    Suit,

    [Question("Which equation was displayed in {0}?", TwoColumns4Answers, ExampleAnswers = ["11 + 11 = 2", "89 + 89 = 187", "11 - 11 = -9", "89 - 11 = 87", "3 × 3 = 0", "9 × 9 = 90"])]
    Equation,

    [Question("Which polyomino was part of C in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites)]
    Polyomino,

    [Discriminator("the Blue Button where N was {0}", Arguments = ["4", "5", "6", "7", "8", "9"], ArgumentGroupSize = 1)]
    DN,

    [Discriminator("the Blue Button where Q was {0}", Arguments = ["Blue", "Green", "Cyan", "Red", "Magenta", "Yellow"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DQ
}

public partial class SouvenirModule
{
    [Handler("BlueButtonModule", "Blue Button", typeof(SBlueButton), "Timwi", AddThe = true)]
    [ManualQuestion("What were N and Q?")]
    [ManualQuestion("What were the polyominoes in C?")]
    [ManualQuestion("What were the equations?")]
    [ManualQuestion("What were the fillings of the sorted suits?")]
    private IEnumerator<SouvenirInstruction> ProcessBlueButton(ModuleData module)
    {
        var comp = GetComponent(module, "BlueButtonScript");

        var colorStageColors = GetArrayField<int>(comp, "_colorStageColors").Get();
        var jumps = GetArrayField<int>(comp, "_jumps").Get(expectedLength: 4, validator: v => v is < 0 or >= 4 ? "expected range 0–3" : null);
        var equations = GetArrayField<string>(comp, "_equations").Get(expectedLength: 4);

        var polyominoPlacements = GetField<IList>(comp, "_polyominoSequence").Get();
        var propPoly = GetProperty<object>(polyominoPlacements[0], "Polyomino", isPublic: true);

        var polyominoesRaw = polyominoPlacements.Cast<object>().Select(x => propPoly.GetFrom(x)).ToArray();
        var fldPolyWidth = GetField<int>(polyominoesRaw[0], "_w");
        var fldPolyHeight = GetField<int>(polyominoesRaw[0], "_h");
        var fldPolyBoolArr = GetArrayField<bool>(polyominoesRaw[0], "_arr");

        (int width, int[] indexes) ConvertPolyomino(object blueButtonPolyomino)
        {
            var width = fldPolyWidth.GetFrom(blueButtonPolyomino);
            var boolArr = fldPolyBoolArr.GetFrom(blueButtonPolyomino);
            return (width, Enumerable.Range(0, boolArr.Length).Where(ix => boolArr[ix]).ToArray());
        }

        var allPolyominoesRaw = (IList) polyominoesRaw[0].GetType().Assembly
            .GetType("BunchOfButtonsLib.BlueButtonPuzzle")
            .GetMethod("GetAllPolyominoes", BindingFlags.Static | BindingFlags.NonPublic)
            .Invoke(null, null);
        var allPolyominoes = allPolyominoesRaw.Cast<object>().Select(ConvertPolyomino).Select(tup => Sprites.GeneratePolyominoSprite(tup.width, tup.indexes)).ToArray();
        var polyominoes = polyominoesRaw.Select(poly => allPolyominoes[allPolyominoesRaw.IndexOf(poly)]).ToArray();

        var colorNames = new[] { "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow" };

        var valN = colorStageColors.Length; // 4–9
        var valQ = colorStageColors[3]; // 0–5, color

        yield return WaitForSolve;

        yield return new Discriminator(SBlueButton.DN, "N", valN, [valN.ToString()]);
        yield return new Discriminator(SBlueButton.DQ, "Q", valQ, [colorNames[valQ]]);

        yield return question(SBlueButton.N).AvoidDiscriminators("N").Answers(valN.ToString());
        yield return question(SBlueButton.Q).AvoidDiscriminators("Q").Answers(colorNames[valQ]);

        for (var i = 0; i < jumps.Length; i++)
            yield return question(SBlueButton.Suit, args: [Ordinal(i + 1)]).Answers(BlueButtonSprites[jumps[i]]);

        yield return question(SBlueButton.Polyomino).Answers(polyominoes, all: allPolyominoes);

        var wrongEquations = new HashSet<string>();
        foreach (var e in equations)
            wrongEquations.Add(e);

        var equationGenMethod = GetMethod<string>(comp, "GenerateEquation", 1);
        while (wrongEquations.Count < equations.Length + 4)
        {
            var offsetParam = Rnd.Range(1, 10) * (Rnd.Range(0, 2) == 1 ? -1 : 1);
            wrongEquations.Add(equationGenMethod.Invoke([offsetParam]));
        }
        yield return question(SBlueButton.Equation).Answers(equations, all: wrongEquations.ToArray());
    }
}
