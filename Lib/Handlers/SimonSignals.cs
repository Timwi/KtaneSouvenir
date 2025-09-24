using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSignals
{
    [SouvenirQuestion("What shape was the {1} arrow in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = nameof(SouvenirModule.SimonSignalsSprites), Arguments = ["red", "green", "blue", "gray"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    ColorToShape,

    [SouvenirQuestion("How many directions did the {1} arrow in {0} have?", TwoColumns4Answers, "3", "4", "5", "6", Arguments = ["red", "green", "blue", "gray"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    ColorToRotations,

    [SouvenirQuestion("What color was the arrow with this shape in {0}?", TwoColumns4Answers, "red", "green", "blue", "gray", UsesQuestionSprite = true, TranslateAnswers = true)]
    ShapeToColor,

    [SouvenirQuestion("How many directions did the arrow with this shape have in {0}?", TwoColumns4Answers, "3", "4", "5", "6", UsesQuestionSprite = true)]
    ShapeToRotations,

    [SouvenirQuestion("What color was the arrow with {1} possible directions in {0}?", TwoColumns4Answers, "red", "green", "blue", "gray", TranslateAnswers = true, Arguments = ["3", "4", "5", "6"], ArgumentGroupSize = 1)]
    RotationsToColor,

    [SouvenirQuestion("What shape was the arrow with {1} possible directions in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = nameof(SouvenirModule.SimonSignalsSprites), Arguments = ["3", "4", "5", "6"], ArgumentGroupSize = 1)]
    RotationsToShape
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonSignalsModule", "Simon Signals", typeof(SSimonSignals), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSignals(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSignalsModule");
        yield return WaitForSolve;

        var numRotations = GetArrayField<int>(comp, "_numRotations").Get(expectedLength: 5);
        var colorsShapes = GetArrayField<int>(comp, "_colorsShapes").Get(expectedLength: 5);

        var colorNames = new[] { "red", "green", "blue", "gray" };
        var any = false;

        for (var i = 0; i < 5; i++)
        {
            // If this arrow has a unique color, we can ask for its shape and its number of rotations
            if (colorsShapes.Count(cs => (cs >> 3) == (colorsShapes[i] >> 3)) == 1)
            {
                yield return question(SSimonSignals.ColorToShape, args: [colorNames[colorsShapes[i] >> 3]]).Answers(SimonSignalsSprites[colorsShapes[i] & 7]);
                yield return question(SSimonSignals.ColorToRotations, args: [colorNames[colorsShapes[i] >> 3]]).Answers(numRotations[i].ToString());
                any = true;
            }

            // If this arrow has a unique shape, we can ask for its color and its number of rotations
            if (colorsShapes.Count(cs => (cs & 7) == (colorsShapes[i] & 7)) == 1)
            {
                yield return question(SSimonSignals.ShapeToColor, questionSprite: SimonSignalsSprites[colorsShapes[i] & 7]).Answers(colorNames[colorsShapes[i] >> 3]);
                yield return question(SSimonSignals.ShapeToRotations, questionSprite: SimonSignalsSprites[colorsShapes[i] & 7]).Answers(numRotations[i].ToString());
                any = true;
            }

            // If this arrow has a unique number of rotations, we can ask for its color and shape
            if (numRotations.Count(nr => nr == numRotations[i]) == 1)
            {
                yield return question(SSimonSignals.RotationsToColor, args: [numRotations[i].ToString()]).Answers(colorNames[colorsShapes[i] >> 3]);
                yield return question(SSimonSignals.RotationsToShape, args: [numRotations[i].ToString()]).Answers(SimonSignalsSprites[colorsShapes[i] & 7]);
                any = true;
            }
        }

        if (!any)
            yield return legitimatelyNoQuestion(module, "none of the arrows had a unique color, shape, or number of directions.");
    }
}
