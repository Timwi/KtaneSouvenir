using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDeafAlley
{
    [SouvenirQuestion("What was the shape generated in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "d", "e", "f", "g", "h", "i", "j", "k", "m", "n", "p", "q", "r", "t", "u", "y", "1", "2", "3", "4", "6", "7", "8", "9", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "[", "]", "{", "}", ":", ";", "“", "‘", "<", ",", ">", ".", "?", "/", "\\"])]
    Shape
}

public partial class SouvenirModule
{
    [SouvenirHandler("deafAlleyModule", "Deaf Alley", typeof(SDeafAlley), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessDeafAlley(ModuleData module)
    {
        var comp = GetComponent(module, "DeafAlleyScript");
        var shapes = GetField<string[]>(comp, "shapes").Get();

        yield return WaitForSolve;

        var selectedShape = GetField<int>(comp, "selectedShape").Get();
        addQuestion(module, Question.DeafAlleyShape, correctAnswers: new[] { shapes[selectedShape] }, preferredWrongAnswers: shapes);
    }
}