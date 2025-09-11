using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFreeParking
{
    [SouvenirQuestion("What was the player token in {0}?", ThreeColumns6Answers, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship", TranslateAnswers = true)]
    Token
}

public partial class SouvenirModule
{
    [SouvenirHandler("freeParking", "Free Parking", typeof(SFreeParking), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessFreeParking(ModuleData module)
    {
        var comp = GetComponent(module, "FreeParkingScript");

        var tokens = GetArrayField<Material>(comp, "tokenOptions", isPublic: true).Get(expectedLength: 7);
        var selected = GetIntField(comp, "tokenIndex").Get(0, tokens.Length - 1);

        yield return WaitForSolve;

        addQuestion(module, Question.FreeParkingToken, correctAnswers: new[] { tokens[selected].name });
    }
}