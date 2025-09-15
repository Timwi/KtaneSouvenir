using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STasqueManaging
{
    [SouvenirQuestion("Where was the starting position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "TasqueManagingSprites")]
    StartingPos
}

public partial class SouvenirModule
{
    [SouvenirHandler("tasqueManaging", "Tasque Managing", typeof(STasqueManaging), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessTasqueManaging(ModuleData module)
    {
        var comp = GetComponent(module, "tasqueManaging");
        yield return WaitForSolve;
        yield return question(STasqueManaging.StartingPos).Answers(TasqueManagingSprites[GetIntField(comp, "startingPosition").Get(min: 0, max: 15)], preferredWrong: TasqueManagingSprites);
    }
}