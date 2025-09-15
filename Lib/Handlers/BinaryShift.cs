using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBinaryShift
{
    [SouvenirQuestion("What was the {1} initial number in {0}?", ThreeColumns6Answers, ExampleAnswers = ["13", "14", "34", "46", "53", "64", "67", "77", "82", "96"], Arguments = ["top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    InitialNumber,
    
    [SouvenirQuestion("What number was selected at stage {1} in {0}?", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true, Arguments = ["0", "1", "2"], ArgumentGroupSize = 1)]
    SelectedNumberPossition,
    
    [SouvenirQuestion("What number was not selected at stage {1} in {0}?", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true, Arguments = ["0", "1", "2"], ArgumentGroupSize = 1)]
    NotSelectedNumberPossition
}

public partial class SouvenirModule
{
    [SouvenirHandler("binary_shift", "Binary Shift", typeof(SBinaryShift), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessBinaryShift(ModuleData module)
    {
        var comp = GetComponent(module, "BinaryShiftModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");

        var allPositions = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" };
        var questions = new List<QandA>();
        for (var position = 0; position < 9; position++)
        {
            var initialNumber = GetMethod<int>(comp, "GetInitialNumber", 1, true).Invoke(position);
            var possibleInitialNumbers = GetProperty<HashSet<int>>(comp, "possibleInitialNumbers", true).Get().Select(n => n.ToString()).ToArray();
            questions.Add(makeQuestion(Question.BinaryShiftInitialNumber, module, formatArgs: new[] { allPositions[position] }, correctAnswers: new[] { initialNumber.ToString() }, preferredWrongAnswers: possibleInitialNumbers));
        }
        var stagesCount = GetProperty<int>(comp, "stagesCount", true).Get();
        for (var stage = 0; stage < stagesCount; stage++)
        {
            var selectedNumberPositions = GetMethod<HashSet<int>>(comp, "GetSelectedNumberPositions", 1, true).Invoke(stage);
            if (selectedNumberPositions.Count < 5)
                questions.Add(makeQuestion(Question.BinaryShiftSelectedNumberPossition, module, formatArgs: new[] { stage.ToString() }, correctAnswers: selectedNumberPositions.Select(p => allPositions[p]).ToArray(), preferredWrongAnswers: allPositions));
            else if (selectedNumberPositions.Count > 5)
                questions.Add(makeQuestion(Question.BinaryShiftNotSelectedNumberPossition, module, formatArgs: new[] { stage.ToString() }, correctAnswers: Enumerable.Range(0, 9).Except(selectedNumberPositions).Select(p => allPositions[p]).ToArray(), preferredWrongAnswers: allPositions));
        }
        addQuestions(module, questions);
    }
}