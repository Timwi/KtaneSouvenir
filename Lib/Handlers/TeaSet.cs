using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STeaSet
{
    [SouvenirQuestion("Which ingredient was displayed {1}, from left to right, in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "TeaSetSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayedIngredients
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSTeaSet", "Tea Set", typeof(STeaSet), "Kuro", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessTeaSet(ModuleData module)
    {
        var comp = GetComponent(module, "TeaSetScript");

        yield return WaitForSolve;

        var displayedIngredients = GetListField<int>(comp, "Order").Get(expectedLength: 8);
        addQuestions(module, displayedIngredients.Select((ing, ix) => makeQuestion(Question.TeaSetDisplayedIngredients, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { TeaSetSprites[ing] }, preferredWrongAnswers: TeaSetSprites)));
    }
}