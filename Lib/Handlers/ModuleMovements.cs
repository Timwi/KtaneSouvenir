using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SModuleMovements
{
    [Question("What was the {1} module shown in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Display
}

public partial class SouvenirModule
{
    [Handler("moduleMovements", "Module Movements", typeof(SModuleMovements), "Hawker")]
    [ManualQuestion("What module was shown for each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessModuleMovements(ModuleData module)
    {
        var comp = GetComponent(module, "moduleMovements");
        var fldStageNum = GetIntField(comp, "stageNumber");
        var fldModuleNum = GetIntField(comp, "moduleNum");
        var currentStage = -1;
        var answers = new Sprite[3];
        var allSprites = GetArrayField<Sprite>(comp, "sprites", isPublic: true).Get(expectedLength: 23).TranslateSprites(400).ToArray();

        while (module.Unsolved)
        {
            var nextStage = fldStageNum.Get();
            if (currentStage != nextStage)
            {
                currentStage = nextStage;
                answers[currentStage] = allSprites[fldModuleNum.Get()];
            }
            yield return null;
        }

        for (var i = 0; i < 2; i++) // We don't ask about the third stage since it stays on the module
            yield return question(SModuleMovements.Display, args: [Ordinal(i + 1)]).Answers(answers[i], all: allSprites);
    }
}
