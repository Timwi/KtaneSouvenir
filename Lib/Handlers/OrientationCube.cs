using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOrientationCube
{
    [SouvenirQuestion("What was the observer’s initial position in {0}?", TwoColumns4Answers, "front", "left", "back", "right", TranslateAnswers = true)]
    InitialObserverPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("OrientationCube", "Orientation Cube", typeof(SOrientationCube), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessOrientationCube(ModuleData module)
    {
        var comp = GetComponent(module, "OrientationModule");

        yield return WaitForSolve;

        var initialVirtualViewAngle = GetField<float>(comp, "initialVirtualViewAngle").Get();
        var initialAnglePos = Array.IndexOf(new[] { 0f, 90f, 180f, 270f }, initialVirtualViewAngle);
        if (initialAnglePos == -1)
            throw new AbandonModuleException($"‘initialVirtualViewAngle’ has unexpected value: {initialVirtualViewAngle}");

        addQuestion(module, Question.OrientationCubeInitialObserverPosition, correctAnswers: new[] { new[] { "front", "left", "back", "right" }[initialAnglePos] });
    }
}