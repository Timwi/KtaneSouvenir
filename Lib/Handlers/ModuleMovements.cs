using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SModuleMovements
{
    [SouvenirQuestion("What was the {1} module shown in {0}?", TwoColumns4Answers, "3D Tunnels", "Alchemy", "Braille", "Button Sequence", "Chord Qualities", "Crackbox", "Functions", "Hunting", "Kudosudoku", "Logic Gates", "Morse-A-Maze", "Pattern Cube", "Planets", "Quintuples", "Schlag den Bomb", "Shapes And Bombs", "Simon Samples", "Simon States", "Symbol Cycle", "Turtle Robot", "Wavetapping", "The Wire", "Yahtzee", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("moduleMovements", "Module Movements", typeof(SModuleMovements), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessModuleMovements(ModuleData module)
    {
        var comp = GetComponent(module, "moduleMovements");
        var fldStageNum = GetIntField(comp, "stageNumber");
        var fldDisplay = GetField<SpriteRenderer>(comp, "display", isPublic: true);
        var currentStage = -1;
        var answers = new string[3];
        var moduleNames = GetArrayField<string>(comp, "modules", true).Get();

        while (module.Unsolved)
        {
            var nextStage = fldStageNum.Get();
            if (currentStage != nextStage)
            {
                currentStage = nextStage;
                answers[currentStage] = fldDisplay.Get().sprite.name;
            }
            yield return null;
        }

        addQuestions(module, answers.Select((ans, i) =>
            makeQuestion(SModuleMovements.Display, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { ans }, preferredWrongAnswers: answers)));
    }
}