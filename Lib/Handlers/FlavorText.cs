using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFlavorText
{
    [SouvenirQuestion("Which module’s flavor text was shown in {0}?", OneColumn4Answers, ExampleAnswers = ["Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button"])]
    Module
}

public partial class SouvenirModule
{
    [SouvenirHandler("FlavorText", "Flavor Text", typeof(SFlavorText), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessFlavorText(ModuleData module)
    {
        var comp = GetComponent(module, "FlavorText");
        yield return WaitForSolve;

        var textOptionList = GetField<IList>(comp, "textOptions").Get();
        var textOption = GetField<object>(comp, "textOption").Get();
        var fldName = GetField<string>(textOption, "name", isPublic: true);
        var moduleNames = Enumerable.Range(0, textOptionList.Count).Select(index => fldName.GetFrom(textOptionList[index])).ToArray();

        yield return question(SFlavorText.Module).Answers(fldName.GetFrom(textOption), preferredWrong: moduleNames);
    }
}