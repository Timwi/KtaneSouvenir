using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUSACycle
{
    [SouvenirQuestion("Which state was displayed in {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "USACycleSprites")]
    Displayed
}

public partial class SouvenirModule
{
    [SouvenirHandler("USACycle", "USA Cycle", typeof(SUSACycle), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessUSACycle(ModuleData module)
    {
        var comp = GetComponent(module, "USACycle");
        var fldStateIndices = GetListField<int>(comp, "StateIndexes");

        yield return WaitForSolve;

        var stateIndices = fldStateIndices.Get(minLength: 4).Where(ix => ix is not 5 and not 49).ToArray();

        //Colorado and Wyoming are practically indistinguishable
        yield return question(SUSACycle.Displayed).Answers(stateIndices.Select(ix => USACycleSprites[ix]).ToArray(), preferredWrong: USACycleSprites.Where((_, pos) => pos is not 5 and not 49).ToArray());
    }
}