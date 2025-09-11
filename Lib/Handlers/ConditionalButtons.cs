using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SConditionalButtons
{
    [SouvenirQuestion("What was the color of this button in {0}?", ThreeColumns6Answers, "black", "blue", "dark green", "light green", "orange", "pink", "purple", "red", "white", "yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("conditionalButtons", "Conditional Buttons", typeof(SConditionalButtons), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessConditionalButtons(ModuleData module)
    {
        var comp = GetComponent(module, "conditionalButtons");
        // Get the colors of the buttons when first starting the module
        var buttonColors = new List<string>();
        foreach (var button in GetListField<KMSelectable>(comp, "Buttons", isPublic: true).Get(expectedLength: 6))
        {
            var buttonColor = button.GetComponent<MeshRenderer>().material.name;
            buttonColors.Add(buttonColor.Remove(buttonColor.IndexOf(" (Instance)")));
        }
        yield return WaitForSolve;
        addQuestions(module, buttonColors.Select((color, ix) => makeQuestion(Question.ConditionalButtonsColors, module, questionSprite: Sprites.GenerateGridSprite(new Coord(3, 2, ix)), correctAnswers: new[] { color })));
    }
}