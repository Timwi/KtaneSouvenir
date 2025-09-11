using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSignals
{
    [SouvenirQuestion("What shape was the {1} arrow in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = nameof(SouvenirModule.SimonSignalsSprites), Arguments = ["red", "green", "blue", "gray"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    ColorToShape,
    
    [SouvenirQuestion("How many directions did the {1} arrow in {0} have?", TwoColumns4Answers, "3", "4", "5", "6", Arguments = ["red", "green", "blue", "gray"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
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
        var qs = new List<QandA>();

        var colorNames = new[] { "red", "green", "blue", "gray" };

        for (var i = 0; i < 5; i++)
        {
            // If this arrow has a unique color, we can ask for its shape and its number of rotations
            if (colorsShapes.Count(cs => (cs >> 3) == (colorsShapes[i] >> 3)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsColorToShape, module,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
                qs.Add(makeQuestion(Question.SimonSignalsColorToRotations, module,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique shape, we can ask for its color and its number of rotations
            if (colorsShapes.Count(cs => (cs & 7) == (colorsShapes[i] & 7)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsShapeToColor, module,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsShapeToRotations, module,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique number of rotations, we can ask for its color and shape
            if (numRotations.Count(nr => nr == numRotations[i]) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToColor, module,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToShape, module,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
            }
        }

        if (qs.Count == 0)
            legitimatelyNoQuestion(module, "none of the arrows had a unique color, shape, or number of directions.");
        else
            addQuestions(module, qs);
    }
}